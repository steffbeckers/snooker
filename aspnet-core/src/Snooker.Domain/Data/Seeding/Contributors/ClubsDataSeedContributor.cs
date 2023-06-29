using Newtonsoft.Json;
using Snooker.Clubs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.TenantManagement;

namespace Snooker.Data.Seeding.Contributors;

public class ClubsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ClubManager _clubManager;
    private readonly IClubRepository _clubRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ITenantRepository _tenantRepository;

    public ClubsDataSeedContributor(
        ClubManager clubManager,
        IClubRepository clubRepository,
        IGuidGenerator guidGenerator,
        ITenantRepository tenantRepository)
    {
        _clubManager = clubManager;
        _clubRepository = clubRepository;
        _guidGenerator = guidGenerator;
        _tenantRepository = tenantRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (!context.TenantId.HasValue)
        {
            return;
        }

        Tenant tenant = await _tenantRepository.GetAsync(context.TenantId.Value);

        if (tenant.Name == "Limburg")
        {
            string limburgClubsJson = await File.ReadAllTextAsync("Data/Seeding/Clubs/snookerlimburg.be.json");
            List<WebScraper.Clubs.Club> limburgClubs = JsonConvert.DeserializeObject<List<WebScraper.Clubs.Club>>(limburgClubsJson);

            foreach (WebScraper.Clubs.Club limburgClub in limburgClubs)
            {
            }

            //Club? clubBiljartLounge = await _clubRepository.FindAsync(x => x.Name == "Biljart Lounge");

            //if (clubBiljartLounge == null)
            //{
            //    clubBiljartLounge = await _clubManager.CreateAsync(
            //        id: Guid.Parse("59dd095c-a5d2-4acc-92e7-c953a19da848"),
            //        name: "Biljart Lounge");
            //    clubBiljartLounge.Email = "hendrik.destaelen@skynet.be";
            //    clubBiljartLounge.PhoneNumber = "011 88 48 88";
            //    clubBiljartLounge.Website = "https://biljartlounge.be";
            //    clubBiljartLounge.Address = new Address()
            //    {
            //        Street = "Tramstraat",
            //        Number = "59",
            //        PostalCode = "3800",
            //        City = "St-Truiden"
            //    };

            //    await _clubRepository.InsertAsync(clubBiljartLounge);
            //}

            //Club? clubBuckingham = await _clubRepository.FindAsync(x => x.Name == "Buckingham");

            //if (clubBuckingham == null)
            //{
            //    clubBuckingham = await _clubManager.CreateAsync(
            //        id: Guid.Parse("14532c33-ea60-4039-9e38-a4470fdb9234"),
            //        name: "Buckingham");
            //    clubBuckingham.Email = "interclub@buckingham.be";
            //    clubBuckingham.PhoneNumber = "014 31 58 27";
            //    clubBuckingham.Website = "https://www.buckingham.be";
            //    clubBuckingham.Address = new Address()
            //    {
            //        Street = "Rozenberg",
            //        Number = "157",
            //        PostalCode = "2400",
            //        City = "Mol"
            //    };

            //    await _clubRepository.InsertAsync(clubBuckingham);
            //}

            //Club? clubDeKreeft = await _clubRepository.FindAsync(x => x.Name == "De Kreeft");

            //if (clubDeKreeft == null)
            //{
            //    clubDeKreeft = await _clubManager.CreateAsync(
            //        id: Guid.Parse("77f45ae4-6ab6-4eba-a60e-defc6c04e22b"),
            //        name: "De Kreeft");
            //    clubDeKreeft.Email = "madeleine@russell-nv.com";
            //    clubDeKreeft.PhoneNumber = "089 49 28 77";
            //    clubDeKreeft.Website = "https://www.kreeftsnooker.com";
            //    clubDeKreeft.Address = new Address()
            //    {
            //        Street = "Bilzersteenweg",
            //        Number = "2",
            //        PostalCode = "3730",
            //        City = "Hoeselt"
            //    };

            //    await _clubRepository.InsertAsync(clubDeKreeft);
            //}

            //Club? clubDeMaxx = await _clubRepository.FindAsync(x => x.Name == "De Maxx");

            //if (clubDeMaxx == null)
            //{
            //    clubDeMaxx = await _clubManager.CreateAsync(
            //        id: Guid.Parse("0a39ca72-58d0-4097-9c52-1458edb7d615"),
            //        name: "De Maxx");
            //    clubDeMaxx.Email = "wendyjans170@gmail.com";
            //    clubDeMaxx.PhoneNumber = "011 52 42 46";
            //    clubDeMaxx.Website = "https://www.facebook.com/profile.php?id=100057511595352";
            //    clubDeMaxx.Address = new Address()
            //    {
            //        Street = "Groenstraat",
            //        Number = "9",
            //        PostalCode = "3910",
            //        City = "Pelt"
            //    };

            //    await _clubRepository.InsertAsync(clubDeMaxx);
            //}

            //Club? clubHappySnooker = await _clubRepository.FindAsync(x => x.Name == "Happy Snooker");

            //if (clubHappySnooker == null)
            //{
            //    clubHappySnooker = await _clubManager.CreateAsync(
            //        id: Guid.Parse("7754fb2b-e866-49d6-be39-3ed7b608975b"),
            //        name: "Happy Snooker");
            //    clubHappySnooker.Email = "jans.danny@live.be";
            //    clubHappySnooker.PhoneNumber = "011 27 50 92";
            //    clubHappySnooker.Website = "https://www.thehappysnooker.be";
            //    clubHappySnooker.Address = new Address()
            //    {
            //        Street = "Oude Luikerbaan",
            //        Number = "81 A",
            //        PostalCode = "3500",
            //        City = "Hasselt"
            //    };

            //    await _clubRepository.InsertAsync(clubHappySnooker);
            //}

            //Club? clubNRG = await _clubRepository.FindAsync(x => x.Name == "NRG");

            //if (clubNRG == null)
            //{
            //    clubNRG = await _clubManager.CreateAsync(
            //        id: Guid.Parse("17a76f42-dcc2-4ba6-824d-6cd232ea64fe"),
            //        name: "NRG");
            //    clubNRG.Email = "ronnie.dereydt@telenet.be";
            //    clubNRG.PhoneNumber = "011 42 89 93";
            //    clubNRG.Website = "https://www.nrgfitness.be/club/nrg-fitness-paal";
            //    clubNRG.Address = new Address()
            //    {
            //        Street = "Diestersesteenweg",
            //        Number = "71",
            //        PostalCode = "3583",
            //        City = "Paal"
            //    };

            //    await _clubRepository.InsertAsync(clubNRG);
            //}

            //Club? clubRespot = await _clubRepository.FindAsync(x => x.Name == "Re-spot");

            //if (clubRespot == null)
            //{
            //    clubRespot = await _clubManager.CreateAsync(
            //        id: Guid.Parse("b12b4841-befe-4ec2-be6d-da6e496cf11a"),
            //        name: "Re-spot");
            //    clubRespot.Email = "info@respot.be";
            //    clubRespot.PhoneNumber = "011 91 89 63";
            //    clubRespot.Website = "https://www.respot.be";
            //    clubRespot.Address = new Address()
            //    {
            //        Street = "Heuvenstraat",
            //        Number = "116",
            //        PostalCode = "3520",
            //        City = "Zonhoven"
            //    };

            //    await _clubRepository.InsertAsync(clubRespot);
            //}

            //Club? clubRileyInn = await _clubRepository.FindAsync(x => x.Name == "Riley Inn");

            //if (clubRileyInn == null)
            //{
            //    clubRileyInn = await _clubManager.CreateAsync(
            //        id: Guid.Parse("4be0a735-d147-4fef-9e09-ba1fd5d44331"),
            //        name: "Riley Inn");
            //    clubRileyInn.Email = "robinlatet@gmail.com";
            //    clubRileyInn.PhoneNumber = "089 35 04 76";
            //    clubRileyInn.Website = "http://riley-inn.be";
            //    clubRileyInn.Address = new Address()
            //    {
            //        Street = "Weg naar As",
            //        Number = "93",
            //        PostalCode = "3600",
            //        City = "Genk"
            //    };

            //    await _clubRepository.InsertAsync(clubRileyInn);
            //}

            //Club? clubSnookerSports = await _clubRepository.FindAsync(x => x.Name == "Snooker Sports");

            //if (clubSnookerSports == null)
            //{
            //    clubSnookerSports = await _clubManager.CreateAsync(
            //        id: Guid.Parse("3b90ea93-ccae-4ed1-9839-7d6a147d6b62"),
            //        name: "Snooker Sports");
            //    clubSnookerSports.Email = "snookersports@telenet.be";
            //    clubSnookerSports.PhoneNumber = "089 76 07 93";
            //    clubSnookerSports.Website = "http://www.snookersports.be";
            //    clubSnookerSports.Address = new Address()
            //    {
            //        Street = "Rijksweg",
            //        Number = "360",
            //        PostalCode = "3630",
            //        City = "Maasmechelen"
            //    };

            //    await _clubRepository.InsertAsync(clubSnookerSports);
            //}

            //Club? clubZuma = await _clubRepository.FindAsync(x => x.Name == "Zuma");

            //if (clubZuma == null)
            //{
            //    clubZuma = await _clubManager.CreateAsync(
            //        id: Guid.Parse("9b667210-e94e-47c2-a6db-33b50b774db4"),
            //        name: "Zuma");
            //    clubZuma.Email = "zuma.snooker@gmail.com";
            //    clubZuma.PhoneNumber = "0472 79 06 91";
            //    clubZuma.Website = "https://www.zumasnooker.be";
            //    clubZuma.Address = new Address()
            //    {
            //        Street = "Vinkstraat",
            //        Number = "9/3",
            //        PostalCode = "3990",
            //        City = "Peer"
            //    };

            //    await _clubRepository.InsertAsync(clubZuma);
            //}
        }
    }
}