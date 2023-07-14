using HtmlAgilityPack;
using Snooker.Interclub.Addresses;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Data.Seeding.Limburg.WebScrape;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Players;
using Snooker.Interclub.Seasons;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        // TODO: Remove
        //List<DivisionDso> test = ExtractFromWebsiteInterclubPage("2023-05-01");

        Season? season2223 = await _seasonRepository.FindAsync(x => x.StartDate.Year == 2022 && x.EndDate.Year == 2023);

        if (season2223 == null)
        {
            season2223 = new Season(
                id: _guidGenerator.Create(),
                startDate: new DateTime(2022, 1, 1),
                endDate: new DateTime(2023, 1, 1));

            // Extract data from snookerlimburg.be website
            string websiteCopyDate = "2023-05-01";
            List<DivisionDso> divisionDsos = ExtractFromWebsiteInterclubPage(websiteCopyDate);
            List<ClubDso> clubDsos = ExtractFromWebsiteClubsPage(websiteCopyDate);

            // Add divisions to database
            foreach (DivisionDso divisionDso in divisionDsos)
            {
                Division division = new Division(
                    _guidGenerator.Create(),
                    season2223.Id,
                    divisionDso.Name)
                {
                    SortOrder = divisionDsos.IndexOf(divisionDso) + 1
                };

                divisionDso.Id = division.Id;

                season2223.Divisions.Add(division);
            }

            // Add clubs, teams and players data to database
            foreach (ClubDso clubDso in clubDsos)
            {
                // Add
                Club? club = await _clubRepository.FindAsync(x => x.Name == clubDso.Name);

                if (club == null)
                {
                    club = await _clubManager.CreateAsync(
                        id: _guidGenerator.Create(),
                        name: clubDso.Name);

                    clubDso.Id = club.Id;
                    club.Number = clubDso.Number.ToString();
                    club.Email = clubDso.Email;
                    club.PhoneNumber = clubDso.PhoneNumber;
                    club.Website = clubDso.Website;
                    club.Address = new Address()
                    {
                        Street = clubDso.Address?.Street,
                        Number = clubDso.Address?.Number,
                        PostalCode = clubDso.Address?.PostalCode,
                        City = clubDso.Address?.City
                    };

                    club = await _clubRepository.InsertAsync(club);
                }

                foreach (TeamDso teamDso in clubDso.Teams)
                {
                    Team? team = null;

                    if (teamDso.Name != "Reserven")
                    {
                        DivisionDso division = divisionDsos.Where(x => x.ClubTeamNames.Contains($"{club.Name} {teamDso.Name}")).FirstOrDefault();

                        if (division != null)
                        {
                            team = new Team(_guidGenerator.Create(), division.Id!.Value, club.Id, teamDso.Name)
                            {
                                ClubId = club.Id
                            };
                        }
                    }

                    foreach (PlayerDso playerDso in teamDso.Players)
                    {
                        Player? player = club.Players.FirstOrDefault(x =>
                            x.FirstName == playerDso.FirstName &&
                            x.LastName == playerDso.LastName &&
                            x.DateOfBirth == playerDso.DateOfBirth);

                        if (player == null)
                        {
                            player = new Player(
                                id: _guidGenerator.Create(),
                                firstName: playerDso.FirstName,
                                lastName: playerDso.LastName)
                            {
                                ClubId = club.Id,
                                Class = playerDso.Class,
                                DateOfBirth = playerDso.DateOfBirth
                            };

                            club.Players.Add(player);
                        }

                        if (team != null)
                        {
                            TeamPlayer teamPlayer = new TeamPlayer(
                                id: _guidGenerator.Create(),
                                team.Id,
                                player.Id);

                            team.Players.Add(teamPlayer);
                        }
                    }

                    if (team != null)
                    {
                        club.Teams.Add(team);
                    }
                }
            }

            await _seasonRepository.InsertAsync(season2223);
        }
    }

    private static List<ClubDso> ExtractFromWebsiteClubsPage(string websiteCopyDate)
    {
        HtmlDocument htmlDocumentClubs = new HtmlDocument();
        htmlDocumentClubs.Load($"Data/Seeding/Limburg/WebScrape/snookerlimburg.be/{websiteCopyDate}/Clubs.html");

        List<ClubDso> clubDsos = new List<ClubDso>();

        // Extract club data
        HtmlNodeCollection clubNodes = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id='main-box']/div[2]/div/table/tbody/tr[not(contains(@class,'ploeginfo'))]");

        foreach (HtmlNode clubNode in clubNodes)
        {
            string name = clubNode.ChildNodes[0].InnerText.Trim();
            string website = clubNode.ChildNodes[0].FirstChild.Attributes["href"].Value;
            string number = clubNode.ChildNodes[1].FirstChild.Attributes["id"].Value.Replace("c", string.Empty);
            string addressLine = clubNode.ChildNodes[2].InnerText.Trim();
            string email = clubNode.ChildNodes[4].InnerText.Trim();
            string phoneNumber = clubNode.ChildNodes[3].InnerText.Trim();

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
            HtmlNodeCollection teamNodes = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id='main-box']/div[2]/div/table/tbody/tr[contains(@class,'ploeg-c" + clubDso.Number + "')]");

            foreach (HtmlNode teamNode in teamNodes)
            {
                string teamName = teamNode.SelectSingleNode(".//p[starts-with(@style, 'font-size: 1.6em;')]").InnerText;

                TeamDso team = new TeamDso()
                {
                    Name = teamName
                };

                HtmlNodeCollection playerDivs = teamNode.SelectNodes(".//div[starts-with(@style, 'float:left; clear: none; min-width: 168px;')]");

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
        HtmlDocument htmlDocumentInterclub = new HtmlDocument();
        htmlDocumentInterclub.Load($"Data/Seeding/Limburg/WebScrape/snookerlimburg.be/{websiteCopyDate}/Interclub.html");

        List<DivisionDso> divisionDsos = new List<DivisionDso>();

        HtmlNodeCollection divisionNodes = htmlDocumentInterclub.DocumentNode.SelectNodes("//*[@id='jwts_tab1']/ul/li/a");

        foreach (HtmlNode divisionNode in divisionNodes)
        {
            string name = divisionNode.InnerText.Replace("&nbsp;", string.Empty)
                .Replace("liga", string.Empty)
                .Replace("afdeling", string.Empty);

            DivisionDso divisionDso = new DivisionDso()
            {
                Name = name
            };

            divisionDsos.Add(divisionDso);
        }

        HtmlNodeCollection rankingTableNodes = htmlDocumentInterclub.DocumentNode.SelectNodes("//table[@class='ic-rank']");

        foreach (DivisionDso divisionDso in divisionDsos)
        {
            int divisionDsoIndex = divisionDsos.IndexOf(divisionDso);
            HtmlNode rankingTableNode = rankingTableNodes.ElementAt(divisionDsoIndex);

            divisionDso.ClubTeamNames = rankingTableNode.SelectNodes(".//td[@class='ranknaam']")
                .Select(x => x.InnerText)
                .OrderBy(x => x)
                .ToList();
        }

        HtmlNodeCollection matchResultTableNodes = htmlDocumentInterclub.DocumentNode.SelectNodes("//table[contains(@class,'ic-result')]");

        List<MatchDso> matches = new List<MatchDso>();

        foreach (HtmlNode tableNode in matchResultTableNodes)
        {
            // Get all the rows in the current table
            HtmlNodeCollection rows = tableNode.SelectNodes(".//tr");

            // Skip the first row (header row)
            for (int i = 1; i < rows.Count; i++)
            {
                HtmlNodeCollection cells = rows[i].SelectNodes(".//td");

                // Extract the relevant data from the cells
                string dateString = cells[0].InnerText.Split(' ')[1];
                string homeTeamName = cells[1].InnerText;
                string scoreString = cells[2].InnerText;
                string awayTeamName = cells[3].InnerText;

                // Extract home team score and away team score from scoreString
                int homeTeamScore, awayTeamScore;
                string[] scoreParts = scoreString.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (scoreParts.Length == 2 && int.TryParse(scoreParts[0].Trim(), out homeTeamScore) && int.TryParse(scoreParts[1].Trim(), out awayTeamScore))
                {
                    // Parse the date using a custom format
                    DateTime date;
                    if (!DateTime.TryParseExact(dateString, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        // Handle date parsing error (e.g., log, skip, etc.)
                        Console.WriteLine($"Failed to parse date: {dateString}");
                        continue;
                    }

                    // Create a new MatchDso object and add it to the list
                    MatchDso match = new MatchDso
                    {
                        Date = date,
                        HomeTeamName = homeTeamName,
                        HomeTeamScore = homeTeamScore,
                        AwayTeamName = awayTeamName,
                        AwayTeamScore = awayTeamScore
                    };

                    matches.Add(match);
                }
                else
                {
                    // Handle score parsing error (e.g., log, skip, etc.)
                    Console.WriteLine($"Failed to parse score: {scoreString}");
                    continue;
                }
            }
        }

        return divisionDsos;
    }
}