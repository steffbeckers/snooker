using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TenantManagement;

namespace Snooker.Interclub.Data.Seeding.Contributors;

public class LimburgDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ITenantRepository _tenantRepository;

    public LimburgDataSeedContributor(ITenantRepository tenantRepository)
    {
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

        // TODO: Scrape data from snookerlimburg.be

        //HtmlDocument htmlDocumentClubs = new HtmlDocument();
        //htmlDocumentClubs.Load("Data/snookerlimburg.be/2023-05-01/Clubs.html");

        //List<Club> clubs = new List<Club>();

        //// Extract club rows
        //HtmlNodeCollection clubRows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[not(contains(@class,'ploeginfo'))]");

        //foreach (HtmlNode clubRow in clubRows)
        //{
        //    string name = clubRow.ChildNodes[0].InnerText.Trim();
        //    string website = clubRow.ChildNodes[0].FirstChild.Attributes["href"].Value;
        //    string number = clubRow.ChildNodes[1].FirstChild.Attributes["id"].Value.Replace("c", string.Empty);
        //    string addressLine = clubRow.ChildNodes[2].InnerText.Trim();
        //    string email = clubRow.ChildNodes[4].InnerText.Trim();
        //    string phoneNumber = clubRow.ChildNodes[3].InnerText.Trim();

        //    clubs.Add(new Club()
        //    {
        //        Name = name,
        //        Website = website,
        //        Number = int.Parse(number),
        //        Address = new Address(addressLine),
        //        Email = email,
        //        PhoneNumber = phoneNumber,
        //    });
        //}

        //// Extract players
        //foreach (Club club in clubs)
        //{
        //    HtmlNodeCollection teamRows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[contains(@class,'ploeg-c" + club.Number + "')]");

        //    foreach (HtmlNode teamRow in teamRows)
        //    {
        //        string teamName = teamRow.SelectSingleNode(".//p[starts-with(@style, 'font-size: 1.6em;')]").InnerText;

        //        Team team = new Team()
        //        {
        //            Name = teamName
        //        };

        //        HtmlNodeCollection playerDivs = teamRow.SelectNodes(".//div[starts-with(@style, 'float:left; clear: none; min-width: 168px;')]");

        //        foreach (HtmlNode playerDiv in playerDivs)
        //        {
        //            // Extract the player's image source
        //            //string imageSrc = playerDiv.SelectSingleNode(".//img").GetAttributeValue("src", "");

        //            // Extract the player's details
        //            HtmlNode playerDetailsNode = playerDiv.SelectSingleNode(".//p[contains(@style, 'color: #032800;')]");
        //            string playerDetails = playerDetailsNode.InnerHtml;

        //            // Split the player details into separate parts
        //            string[] detailsParts = playerDetails.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

        //            // Split the player's name into first name and last name
        //            string[] nameParts = detailsParts[0].Trim().Split(' ');
        //            string lastName = string.Join(" ", nameParts[0..^1]);
        //            string firstName = nameParts[^1];

        //            // Extract the player's class, date of birth, etc.
        //            int? playerClass = int.Parse(detailsParts[1].Trim().Replace("klasse: ", string.Empty));
        //            DateTime playerDateOfBirth = DateTime.ParseExact(detailsParts[2].Trim(), "dd-MM-yyyy", null);

        //            team.Players.Add(new Player()
        //            {
        //                FirstName = firstName,
        //                LastName = lastName,
        //                Class = playerClass,
        //                DateOfBirth = playerDateOfBirth,
        //                //Image = imageSrc
        //            });

        //            team.Players = team.Players.OrderBy(x => x.FirstName).ToList();
        //        }

        //        club.Teams.Add(team);
        //    }
        //}

        //foreach (WebScraper.Clubs.Club limburgClub in limburgClubs)
        //{
        //    Club? club = await _clubRepository.FindAsync(x => x.Name == limburgClub.Name);
        //if (club == null)
        //    {
        //        club = await _clubManager.CreateAsync(
        //            id: _guidGenerator.Create(),
        //            name: limburgClub.Name);
        //        club.Number = limburgClub.Number.ToString();
        //        club.Email = limburgClub.Email;
        //        club.PhoneNumber = limburgClub.PhoneNumber;
        //        club.Website = limburgClub.Website;
        //        club.Address = new Address()
        //        {
        //            Street = limburgClub.Address?.Street,
        //            Number = limburgClub.Address?.Number,
        //            PostalCode = limburgClub.Address?.PostalCode,
        //            City = limburgClub.Address?.City
        //        };

        //        foreach (WebScraper.Teams.Team limburgTeam in limburgClub.Teams)
        //        {
        //        Team? team = null;
        //        if (limburgTeam.Name != "Reserven")
        //            {
        //                team = new Team(_guidGenerator.Create(), club.Id, limburgTeam.Name)
        //                {
        //                    ClubId = club.Id
        //                };
        //            }
        //        foreach (WebScraper.Players.Player limburgPlayer in limburgTeam.Players)
        //            {
        //                Player? player = club.Players.FirstOrDefault(x =>
        //                    x.FirstName == limburgPlayer.FirstName &&
        //                    x.LastName == limburgPlayer.LastName &&
        //                    x.DateOfBirth == limburgPlayer.DateOfBirth);

        //                if (player == null)
        //            {
        //                    player = new Player(
        //                        id: _guidGenerator.Create(),
        //                        firstName: limburgPlayer.FirstName,
        //                        lastName: limburgPlayer.LastName)
        //                    {
        //                        ClubId = club.Id,
        //                        Class = limburgPlayer.Class,
        //                        DateOfBirth = limburgPlayer.DateOfBirth
        //                    };

        //                    club.Players.Add(player);
        //                }

        //                if (team != null)
        //            {
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
}