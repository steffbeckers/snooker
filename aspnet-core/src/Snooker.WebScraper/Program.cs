using HtmlAgilityPack;
using Newtonsoft.Json;
using Snooker.WebScraper.Clubs;

namespace Snooker.WebScraper;

public static class Program
{
    public static void Main(string[] args)
    {
        HtmlDocument htmlDocumentClubs = new HtmlDocument();
        htmlDocumentClubs.Load("Data/snookerlimburg.be/2023-05-01/Clubs.html");

        List<Club> clubs = new List<Club>();
        HtmlNodeCollection rows = htmlDocumentClubs.DocumentNode.SelectNodes("//*[@id=\"main-box\"]/div[2]/div/table/tbody/tr[not(contains(@class,'ploeginfo'))]");

        foreach (HtmlNode row in rows)
        {
            string name = row.ChildNodes[0].InnerText.Trim();
            string website = row.ChildNodes[0].FirstChild.Attributes["href"].Value;
            string addressLine = row.ChildNodes[2].InnerText.Trim();
            string email = row.ChildNodes[4].InnerText.Trim();
            string phoneNumber = row.ChildNodes[3].InnerText.Trim();

            clubs.Add(new Club()
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Website = website
            });
        }

        string clubsJson = JsonConvert.SerializeObject(clubs, Formatting.Indented);
        File.WriteAllText("clubs.json", clubsJson);
    }
}