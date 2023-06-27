using HtmlAgilityPack;
using Newtonsoft.Json;
using Snooker.WebScraper.Addresses;
using Snooker.WebScraper.Clubs;
using Snooker.WebScraper.Players;
using Snooker.WebScraper.Teams;

namespace Snooker.WebScraper;

public static class Program
{
    public static void Main(string[] args)
    {
        HtmlDocument htmlDocumentClubs = new HtmlDocument();
        htmlDocumentClubs.Load("Data/snookerlimburg.be/2023-05-01/Clubs.html");

        List<Club> clubs = new List<Club>();

        // Extract club rows
        HtmlNodeCollection clubRows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[not(contains(@class,'ploeginfo'))]");
        foreach (HtmlNode clubRow in clubRows)
        {
            string name = clubRow.ChildNodes[0].InnerText.Trim();
            string website = clubRow.ChildNodes[0].FirstChild.Attributes["href"].Value;
            string number = clubRow.ChildNodes[1].FirstChild.Attributes["id"].Value.Replace("c", string.Empty);
            string addressLine = clubRow.ChildNodes[2].InnerText.Trim();
            string email = clubRow.ChildNodes[4].InnerText.Trim();
            string phoneNumber = clubRow.ChildNodes[3].InnerText.Trim();

            clubs.Add(new Club()
            {
                Name = name,
                Website = website,
                Number = int.Parse(number),
                Address = new Address(addressLine),
                Email = email,
                PhoneNumber = phoneNumber,
            });
        }

        // Extract players
        foreach (Club club in clubs)
        {
            HtmlNodeCollection teamRows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[contains(@class,'ploeg-c" + club.Number + "')]");

            foreach (HtmlNode teamRow in teamRows)
            {
                string teamName = teamRow.SelectSingleNode(".//p[starts-with(@style, 'font-size: 1.6em;')]").InnerText;

                Team team = new Team()
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

                    team.Players.Add(new Player()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Class = playerClass,
                        DateOfBirth = playerDateOfBirth,
                        //Image = imageSrc
                    });

                    team.Players = team.Players.OrderBy(x => x.FirstName).ToList();
                }

                club.Teams.Add(team);
            }
        }

        // Export clubs.json
        string clubsJson = JsonConvert.SerializeObject(clubs, Formatting.Indented);
        File.WriteAllText("clubs.json", clubsJson);
    }
}