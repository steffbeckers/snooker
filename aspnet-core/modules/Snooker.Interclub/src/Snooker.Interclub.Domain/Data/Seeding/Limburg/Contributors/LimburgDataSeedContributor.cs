using HtmlAgilityPack;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Data.Seeding.Limburg.WebScrape;
using Snooker.Interclub.Seasons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.TenantManagement;

namespace Snooker.Interclub.Data.Seeding.Limburg.Contributors;

public class LimburgDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ClubManager _clubManager;
    private readonly IClubRepository _clubRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ISeasonRepository _seasonRepository;
    private readonly ITenantRepository _tenantRepository;

    public LimburgDataSeedContributor(
        ClubManager clubManager,
        IClubRepository clubRepository,
        IGuidGenerator guidGenerator,
        ISeasonRepository seasonRepository,
        ITenantRepository tenantRepository)
    {
        _clubManager = clubManager;
        _clubRepository = clubRepository;
        _guidGenerator = guidGenerator;
        _seasonRepository = seasonRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (!context.TenantId.HasValue)
        {
            return;
        }

        Tenant tenant = await _tenantRepository.GetAsync(context.TenantId.Value);

        if (tenant.Name != "Limburg")
        {
            return;
        }

        Season? season2223 = await _seasonRepository.FindAsync(x => x.StartDate.Year == 2022 && x.EndDate.Year == 2023);

        if (season2223 == null)
        {
            season2223 = new Season(
                id: _guidGenerator.Create(),
                startDate: new DateTime(2022, 1, 1),
                endDate: new DateTime(2023, 1, 1));

            season2223 = await _seasonRepository.InsertAsync(season2223, autoSave: true);

            // Extract data from snookerlimburg.be website
            string websiteCopyDate = "2023-05-01";
            List<DivisionDso> divisionDsos = ExtractFromWebsiteInterclubPage(websiteCopyDate);
            List<ClubDso> clubDsos = ExtractFromWebsiteClubsPage(websiteCopyDate);
        }

        // Add clubs, teams and players data to database
        //foreach (ClubDso clubDso in clubDsos)
        //{
        //    // Add
        //    Club? club = await _clubRepository.FindAsync(x => x.Name == clubDso.Name);

        //    if (club == null)
        //    {
        //        club = await _clubManager.CreateAsync(
        //            id: _guidGenerator.Create(),
        //            name: clubDso.Name);
        //        club.Number = clubDso.Number.ToString();
        //        club.Email = clubDso.Email;
        //        club.PhoneNumber = clubDso.PhoneNumber;
        //        club.Website = clubDso.Website;
        //        club.Address = new Address()
        //        {
        //            Street = clubDso.Address?.Street,
        //            Number = clubDso.Address?.Number,
        //            PostalCode = clubDso.Address?.PostalCode,
        //            City = clubDso.Address?.City
        //        };

        //        foreach (TeamDso teamDso in clubDso.Teams)
        //        {
        //            Team? team = null;
        //            if (teamDso.Name != "Reserven")
        //            {
        //                team = new Team(_guidGenerator.Create(), club.Id, teamDso.Name)
        //                {
        //                    ClubId = club.Id
        //                };
        //            }

        //            foreach (PlayerDso playerDso in teamDso.Players)
        //            {
        //                Player? player = club.Players.FirstOrDefault(x =>
        //                    x.FirstName == playerDso.FirstName &&
        //                    x.LastName == playerDso.LastName &&
        //                    x.DateOfBirth == playerDso.DateOfBirth);

        //                if (player == null)
        //                {
        //                    player = new Player(
        //                        id: _guidGenerator.Create(),
        //                        firstName: playerDso.FirstName,
        //                        lastName: playerDso.LastName)
        //                    {
        //                        ClubId = club.Id,
        //                        Class = playerDso.Class,
        //                        DateOfBirth = playerDso.DateOfBirth
        //                    };

        //                    club.Players.Add(player);
        //                }

        //                if (team != null)
        //                {
        //                    TeamPlayer teamPlayer = new TeamPlayer(
        //                        id: _guidGenerator.Create(),
        //                        team.Id,
        //                        player.Id);

        //                    team.Players.Add(teamPlayer);
        //                }
        //            }

        //            if (team != null)
        //            {
        //                club.Teams.Add(team);
        //            }
        //        }

        //        await _clubRepository.InsertAsync(club);
        //    }
        //}
    }

    private static List<ClubDso> ExtractFromWebsiteClubsPage(string websiteCopyDate)
    {
        HtmlDocument htmlDocumentClubs = new HtmlDocument();
        htmlDocumentClubs.Load($"Data/Seeding/Limburg/snookerlimburg.be/{websiteCopyDate}/Clubs.html");

        List<ClubDso> clubDsos = new List<ClubDso>();

        // Extract club data
        HtmlNodeCollection clubRows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[not(contains(@class,'ploeginfo'))]");

        foreach (HtmlNode clubRow in clubRows)
        {
            string name = clubRow.ChildNodes[0].InnerText.Trim();
            string website = clubRow.ChildNodes[0].FirstChild.Attributes["href"].Value;
            string number = clubRow.ChildNodes[1].FirstChild.Attributes["id"].Value.Replace("c", string.Empty);
            string addressLine = clubRow.ChildNodes[2].InnerText.Trim();
            string email = clubRow.ChildNodes[4].InnerText.Trim();
            string phoneNumber = clubRow.ChildNodes[3].InnerText.Trim();

            clubDsos.Add(new ClubDso()
            {
                Name = name,
                Website = website,
                Number = int.Parse(number),
                Address = new AddressDso(addressLine),
                Email = email,
                PhoneNumber = phoneNumber,
            });
        }

        // Extract team and player data
        foreach (ClubDso clubDso in clubDsos)
        {
            HtmlNodeCollection teamRows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[contains(@class,'ploeg-c" + clubDso.Number + "')]");

            foreach (HtmlNode teamRow in teamRows)
            {
                string teamName = teamRow.SelectSingleNode(".//p[starts-with(@style, 'font-size: 1.6em;')]").InnerText;

                TeamDso team = new TeamDso()
                {
                    Name = teamName
                };

                HtmlNodeCollection playerDivs = teamRow.SelectNodes(".//div[starts-with(@style, 'float:left; clear: none; min-width: 168px;')]");

                foreach (HtmlNode playerDiv in playerDivs)
                {
                    // Extract the player's image source
                    //string imageSrc = playerDiv.SelectSingleNode(".//img").GetAttributeValue("src", "");

                    // Extract the player's details
                    HtmlNode playerDetailsNode = playerDiv.SelectSingleNode(".//p[contains(@style, 'color: #032800;')]");
                    string playerDetails = playerDetailsNode.InnerHtml;

                    // Split the player details into separate parts
                    string[] detailsParts = playerDetails.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

                    // Split the player's name into first name and last name
                    string[] nameParts = detailsParts[0].Trim().Split(' ');
                    string lastName = string.Join(" ", nameParts[0..^1]);
                    string firstName = nameParts[^1];

                    // Extract the player's class, date of birth, etc.
                    int? playerClass = int.Parse(detailsParts[1].Trim().Replace("klasse: ", string.Empty));
                    DateTime playerDateOfBirth = DateTime.ParseExact(detailsParts[2].Trim(), "dd-MM-yyyy", null);

                    team.Players.Add(new PlayerDso()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Class = playerClass,
                        DateOfBirth = playerDateOfBirth,
                        //Image = imageSrc
                    });

                    team.Players = team.Players.OrderBy(x => x.FirstName).ToList();
                }

                clubDso.Teams.Add(team);
            }
        }

        return clubDsos;
    }

    private List<DivisionDso> ExtractFromWebsiteInterclubPage(string websiteCopyDate)
    {
        HtmlDocument htmlDocumentClubs = new HtmlDocument();
        htmlDocumentClubs.Load($"Data/Seeding/Limburg/snookerlimburg.be/{websiteCopyDate}/Interclub.html");

        List<DivisionDso> divisionDsos = new List<DivisionDso>();

        return divisionDsos;
    }
}