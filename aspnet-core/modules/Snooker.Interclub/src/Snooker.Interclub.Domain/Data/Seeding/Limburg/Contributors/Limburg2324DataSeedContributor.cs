using HtmlAgilityPack;
using Snooker.Interclub.Addresses;
using Snooker.Interclub.Clubs;
using Snooker.Interclub.Data.Seeding.Limburg.WebScrape;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Frames;
using Snooker.Interclub.Matches;
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

public class Limburg2324DataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private const string _websiteCopyDate = "2024-05-23";
    private readonly ClubManager _clubManager;
    private readonly IClubRepository _clubRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ISeasonRepository _seasonRepository;
    private readonly ITenantRepository _tenantRepository;

    public Limburg2324DataSeedContributor(
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

        Season? season = await _seasonRepository.FindAsync(x => x.StartDate.Year == 2023 && x.EndDate.Year == 2024);

        if (season == null)
        {
            season = new Season(
                id: _guidGenerator.Create(),
                startDate: new DateTime(2023, 1, 1),
                endDate: new DateTime(2024, 1, 1));

            // Extract data from snookerlimburg.be website
            List<DivisionDso> divisionDsos = ExtractFromWebsiteInterclubPage();
            List<ClubDso> clubDsos = ExtractFromWebsiteClubsPage();

            // Add divisions to database
            foreach (DivisionDso divisionDso in divisionDsos)
            {
                Division division = new Division(
                    _guidGenerator.Create(),
                    season,
                    divisionDso.Name)
                {
                    SortOrder = divisionDsos.IndexOf(divisionDso) + 1
                };

                divisionDso.Id = division.Id;

                season.Divisions.Add(division);
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
                        DivisionDso divisionDso = divisionDsos.Where(x => x.ClubTeamNames.Contains($"{club.Name} {teamDso.Name}")).FirstOrDefault();

                        if (divisionDso != null)
                        {
                            Division division = season.Divisions.First(x => x.Id == divisionDso.Id);

                            team = new Team(
                                _guidGenerator.Create(),
                                division,
                                club,
                                teamDso.Name)
                            {
                                ClubId = club.Id
                            };

                            teamDso.Id = team.Id;

                            division.Teams.Add(team);
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
                                team,
                                player);

                            team.Players.Add(teamPlayer);
                        }
                    }

                    if (team != null)
                    {
                        club.Teams.Add(team);
                    }
                }
            }

            // Add matches to division
            foreach (DivisionDso divisionDso in divisionDsos)
            {
                Division division = season.Divisions.First(x => x.Id == divisionDso.Id);

                foreach (MatchDso matchDso in divisionDso.Matches)
                {
                    Team homeTeam = division.Teams.FirstOrDefault(x => x.ClubTeamName == matchDso.HomeTeamName);

                    if (homeTeam == null)
                    {
                        Console.WriteLine($"Failed to find home team {matchDso.HomeTeamName}");
                        continue;
                    }

                    Team awayTeam = division.Teams.FirstOrDefault(x => x.ClubTeamName == matchDso.AwayTeamName);

                    if (awayTeam == null)
                    {
                        Console.WriteLine($"Failed to find away team {matchDso.AwayTeamName}");
                        continue;
                    }

                    Match match = new Match(
                        _guidGenerator.Create(),
                        homeTeam,
                        awayTeam)
                    {
                        Date = matchDso.Date,
                        HomeTeamScore = matchDso.HomeTeamScore,
                        AwayTeamScore = matchDso.AwayTeamScore
                    };

                    matchDso.Id = match.Id;

                    foreach (string homeTeamPlayerName in matchDso.HomeTeamPlayerNames)
                    {
                        MatchTeamPlayer? matchTeamPlayer = null;

                        TeamPlayer? teamPlayer = homeTeam.Players.FirstOrDefault(x => x.Player.LastNameFirstName == homeTeamPlayerName);

                        if (teamPlayer != null)
                        {
                            matchTeamPlayer = new MatchTeamPlayer(
                                _guidGenerator.Create(),
                                match,
                                homeTeam,
                                teamPlayer.Player)
                            {
                                IsCaptain = teamPlayer.IsCaptain
                            };
                        }
                        else
                        {
                            Player? player = homeTeam.Club.Players.FirstOrDefault(x => x.LastNameFirstName == homeTeamPlayerName);

                            if (player != null)
                            {
                                matchTeamPlayer = new MatchTeamPlayer(
                                    _guidGenerator.Create(),
                                    match,
                                    homeTeam,
                                    player);
                            }
                        }

                        if (matchTeamPlayer != null)
                        {
                            match.TeamPlayers.Add(matchTeamPlayer);
                        }
                    }

                    foreach (string awayTeamPlayerName in matchDso.AwayTeamPlayerNames)
                    {
                        MatchTeamPlayer? matchTeamPlayer = null;

                        TeamPlayer? teamPlayer = awayTeam.Players.FirstOrDefault(x => x.Player.LastNameFirstName == awayTeamPlayerName);

                        if (teamPlayer != null)
                        {
                            matchTeamPlayer = new MatchTeamPlayer(
                                _guidGenerator.Create(),
                                match,
                                awayTeam,
                                teamPlayer.Player)
                            {
                                IsCaptain = teamPlayer.IsCaptain
                            };
                        }
                        else
                        {
                            Player? player = awayTeam.Club.Players.FirstOrDefault(x => x.LastNameFirstName == awayTeamPlayerName);

                            if (player != null)
                            {
                                matchTeamPlayer = new MatchTeamPlayer(
                                    _guidGenerator.Create(),
                                    match,
                                    awayTeam,
                                    player);
                            }
                        }

                        if (matchTeamPlayer != null)
                        {
                            match.TeamPlayers.Add(matchTeamPlayer);
                        }
                    }

                    foreach (FrameDso frameDso in matchDso.Frames)
                    {
                        MatchTeamPlayer? homeTeamPlayer = match.HomeTeamPlayers.FirstOrDefault(x => x.Player.LastNameFirstName == frameDso.HomeTeamPlayerName);
                        MatchTeamPlayer? awayTeamPlayer = match.AwayTeamPlayers.FirstOrDefault(x => x.Player.LastNameFirstName == frameDso.AwayTeamPlayerName);

                        Frame frame = new Frame(
                            _guidGenerator.Create(),
                            match,
                            homeTeamPlayer.Player,
                            awayTeamPlayer.Player)
                        {
                            HomePlayerScore = frameDso.HomeTeamPlayerScore,
                            AwayPlayerScore = frameDso.AwayTeamPlayerScore
                        };

                        foreach (int homeTeamPlayerBreak in frameDso.HomeTeamPlayerBreaks)
                        {
                            frame.Breaks.Add(
                                new Break(
                                    _guidGenerator.Create(),
                                    frame,
                                    homeTeamPlayer.Player,
                                    homeTeamPlayerBreak));
                        }

                        foreach (int awayTeamPlayerBreak in frameDso.AwayTeamPlayerBreaks)
                        {
                            frame.Breaks.Add(
                                new Break(
                                    _guidGenerator.Create(),
                                    frame,
                                    awayTeamPlayer.Player,
                                    awayTeamPlayerBreak));
                        }

                        match.Frames.Add(frame);
                    }

                    division.Matches.Add(match);
                }
            }

            await _seasonRepository.InsertAsync(season);
        }
    }

    private static List<ClubDso> ExtractFromWebsiteClubsPage()
    {
        HtmlDocument htmlDocumentClubs = new HtmlDocument();
        htmlDocumentClubs.Load($"Data/Seeding/Limburg/WebScrape/snookerlimburg.be/{_websiteCopyDate}/Clubs.html");

        List<ClubDso> clubDsos = new List<ClubDso>();

        // Extract club data
        HtmlNodeCollection clubNodes = htmlDocumentClubs.DocumentNode.SelectNodes("//table[contains(@class,'txt-clubs')]/tbody/tr[not(contains(@class,'ploeginfo'))]");

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
            HtmlNodeCollection teamNodes = htmlDocumentClubs.DocumentNode.SelectNodes("//table[contains(@class,'txt-clubs')]/tbody/tr[contains(@class,'ploeg-c" + clubDso.Number + "')]");

            foreach (HtmlNode teamNode in teamNodes)
            {
                string teamName = teamNode.SelectSingleNode(".//h5").InnerText
                    .Replace("&nbsp;", string.Empty)
                    .Split(" (")
                    .First();

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

    private List<DivisionDso> ExtractFromWebsiteInterclubPage()
    {
        List<DivisionDso> divisionDsos = new List<DivisionDso>();

        HtmlDocument htmlDocumentInterclub = new HtmlDocument();
        htmlDocumentInterclub.Load($"Data/Seeding/Limburg/WebScrape/snookerlimburg.be/{_websiteCopyDate}/Interclub.html");

        // Extract divisions
        HtmlNodeCollection divisionTabNodes = htmlDocumentInterclub.DocumentNode.SelectNodes("//div[@id='jwts_tab1']/div[contains(@class,'jwts_tabbertab')]");

        foreach (HtmlNode divisionTabNode in divisionTabNodes)
        {
            DivisionDso divisionDso = new DivisionDso();

            // Name
            HtmlNode nameNode = divisionTabNode.SelectSingleNode(".//h2[@class='jwts_heading']/a");
            divisionDso.Name = nameNode.InnerText.Replace("&nbsp;", string.Empty)
                .Replace("liga", string.Empty)
                .Replace("afdeling", string.Empty);

            // Club team names
            HtmlNode rankingTableNode = divisionTabNode.SelectSingleNode(".//table[@class='ic-rank']");
            divisionDso.ClubTeamNames = rankingTableNode.SelectNodes(".//td[@class='ranknaam']")
                .Select(x => x.InnerText)
                .OrderBy(x => x)
                .ToList();

            // Matches
            HtmlNodeCollection matchResultTableNodes = divisionTabNode.SelectNodes(".//table[contains(@class,'ic-result')]");

            foreach (HtmlNode matchResultTableNode in matchResultTableNodes)
            {
                // Get all the rows in the current table
                HtmlNodeCollection rows = matchResultTableNode.SelectNodes(".//tr");

                // Skip the first row (header row)
                for (int i = 1; i < rows.Count; i++)
                {
                    HtmlNode row = rows[i];

                    HtmlNode? detailBtnNode = row.SelectSingleNode(".//span[@class='detailbtn']");
                    string? detailId = detailBtnNode?.Id;
                    if (detailId == null)
                    {
                        continue;
                    }

                    HtmlNodeCollection cells = row.SelectNodes(".//td");

                    // Extract the relevant data from the cells
                    string scoreString = cells[2].InnerText;
                    if (scoreString.Contains("&nbps;"))
                    {
                        continue;
                    }

                    string dateString = cells[0].InnerText.Split(' ')[1];
                    string homeTeamName = cells[1].InnerText;
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
                        MatchDso matchDso = new MatchDso()
                        {
                            Date = date,
                            HomeTeamName = homeTeamName,
                            HomeTeamScore = homeTeamScore,
                            AwayTeamName = awayTeamName,
                            AwayTeamScore = awayTeamScore,
                            DetailId = !string.IsNullOrEmpty(detailId) ? $"dtl{detailId}" : null
                        };

                        if (!string.IsNullOrEmpty(matchDso.DetailId))
                        {
                            HtmlNode matchDetailNode = htmlDocumentInterclub.DocumentNode.SelectSingleNode($"//div[@id='{matchDso.DetailId}']/table/tbody");

                            if (matchDetailNode != null)
                            {
                                matchDso.HomeTeamPlayerNames = matchDetailNode.SelectNodes(".//td[@class='tsp']/span").Select(x => x.InnerText).ToList();
                                matchDso.AwayTeamPlayerNames = matchDetailNode.SelectNodes(".//td[@class='usp']/span").Select(x => x.InnerText).ToList();

                                // Frames
                                HtmlNodeCollection matrixNodes = matchDetailNode.SelectNodes(".//div[@class='mtrx']");

                                for (int j = 0; j < matchDso.HomeTeamPlayerNames.Count; j++)
                                {
                                    for (int k = 0; k < matchDso.AwayTeamPlayerNames.Count; k++)
                                    {
                                        int matrixIndex = j * matchDso.AwayTeamPlayerNames.Count + k;
                                        HtmlNode matrixNode = matrixNodes[matrixIndex];

                                        HtmlNodeCollection frameResultNodes = matrixNode.SelectNodes(".//span[contains(@class,'fscore')]");
                                        foreach (HtmlNode frameResultNode in frameResultNodes)
                                        {
                                            int[] scores = frameResultNode.InnerText.Split(" - ").Select(int.Parse).ToArray();

                                            FrameDso frameDso = new FrameDso()
                                            {
                                                HomeTeamPlayerName = matchDso.HomeTeamPlayerNames[j],
                                                HomeTeamPlayerScore = scores[0],
                                                AwayTeamPlayerName = matchDso.AwayTeamPlayerNames[k],
                                                AwayTeamPlayerScore = scores[1]
                                            };

                                            // Breaks
                                            HtmlNode homePlayerBreakNode = frameResultNode.SelectSingleNode("preceding-sibling::*[1][self::span[@class='brl']]");
                                            if (homePlayerBreakNode != null)
                                            {
                                                frameDso.HomeTeamPlayerBreaks = homePlayerBreakNode.InnerText
                                                    .Replace("(br ", string.Empty)
                                                    .Replace(")", string.Empty)
                                                    .Split("+")
                                                    .Select(int.Parse)
                                                    .ToList();
                                            }

                                            HtmlNode awayPlayerBreakNode = frameResultNode.SelectSingleNode("following-sibling::*[1][self::span[@class='brr']]");
                                            if (awayPlayerBreakNode != null)
                                            {
                                                frameDso.AwayTeamPlayerBreaks = awayPlayerBreakNode.InnerText
                                                    .Replace("(br ", string.Empty)
                                                    .Replace(")", string.Empty)
                                                    .Split("+")
                                                    .Select(int.Parse)
                                                    .ToList();
                                            }

                                            matchDso.Frames.Add(frameDso);
                                        }
                                    }
                                }
                            }
                        }

                        divisionDso.Matches.Add(matchDso);
                    }
                    else
                    {
                        // Handle score parsing error (e.g., log, skip, etc.)
                        Console.WriteLine($"Failed to parse score: {scoreString}");
                        continue;
                    }
                }
            }

            divisionDsos.Add(divisionDso);
        }

        return divisionDsos;
    }
}