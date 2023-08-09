using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Snooker.Interclub.Seasons;

public class SeasonsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ClubManager _clubManager;
    private readonly IClubRepository _clubRepository;
    private readonly ISeasonRepository _seasonRepository;

    public SeasonsDataSeedContributor(
        ClubManager clubManager,
        IClubRepository clubRepository,
        ISeasonRepository seasonRepository)
    {
        _clubManager = clubManager;
        _clubRepository = clubRepository;
        _seasonRepository = seasonRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        Season season20222023 = new Season(
            id: Guid.Parse("1877aa91-3906-48f0-a4eb-eb7dc9358835"),
            startDate: new DateTime(2022, 1, 1),
            endDate: new DateTime(2023, 1, 1));

        Club clubBiljartLounge = await _clubManager.CreateAsync(
            id: Guid.Parse("d1610828-8df5-4ced-8bf3-d876f6207691"),
            name: "Biljart Lounge");
        clubBiljartLounge.NumberOfTables = 4;
        await _clubRepository.InsertAsync(clubBiljartLounge);

        Club clubBuckingham = await _clubManager.CreateAsync(
            id: Guid.Parse("1252eaad-0efa-433f-a598-4909158de769"),
            name: "Buckingham");
        clubBuckingham.NumberOfTables = 8;
        await _clubRepository.InsertAsync(clubBuckingham);

        Club clubDeKreeft = await _clubManager.CreateAsync(
            id: Guid.Parse("c95db223-c2c3-49e6-b0cd-d823327d1814"),
            name: "De Kreeft");
        clubDeKreeft.NumberOfTables = 6;
        await _clubRepository.InsertAsync(clubDeKreeft);

        Club clubDeMaxx = await _clubManager.CreateAsync(
            id: Guid.Parse("a0d1964b-92c1-42dd-a5a9-a45a21f1279c"),
            name: "De Maxx");
        clubDeMaxx.NumberOfTables = 10;
        await _clubRepository.InsertAsync(clubDeMaxx);

        Club clubHappySnooker = await _clubManager.CreateAsync(
            id: Guid.Parse("9a03e899-1054-4e48-86e3-ff7250d48eb5"),
            name: "Happy Snooker");
        clubHappySnooker.NumberOfTables = 5;
        await _clubRepository.InsertAsync(clubHappySnooker);

        Club clubNRG = await _clubManager.CreateAsync(
            id: Guid.Parse("d72092b9-1476-4954-914a-2bfb1c8c73e6"),
            name: "NRG");
        clubNRG.NumberOfTables = 3;
        await _clubRepository.InsertAsync(clubNRG);

        Club clubReSpot = await _clubManager.CreateAsync(
            id: Guid.Parse("e6071606-563e-4e73-beb6-962dfe3df769"),
            name: "Re-Spot");
        clubReSpot.NumberOfTables = 6;
        await _clubRepository.InsertAsync(clubReSpot);

        Club clubRileyInn = await _clubManager.CreateAsync(
            id: Guid.Parse("19d70aac-ba91-4a53-84b4-69cf4e694f0a"),
            name: "Riley Inn");
        clubRileyInn.NumberOfTables = 8;
        await _clubRepository.InsertAsync(clubRileyInn);

        Club clubSnookerSports = await _clubManager.CreateAsync(
            id: Guid.Parse("95adc0ff-e6ab-4d52-aa64-82f4849228fc"),
            name: "Snooker Sports");
        clubSnookerSports.NumberOfTables = 3;
        await _clubRepository.InsertAsync(clubSnookerSports);

        Club clubZuma = await _clubManager.CreateAsync(
            id: Guid.Parse("e8fb5f9d-b5a5-4456-83bf-43282490e87b"),
            name: "Zuma");
        clubZuma.NumberOfTables = 6;
        await _clubRepository.InsertAsync(clubZuma);

        Division divisionEre = new Division(
            id: Guid.Parse("3c7923fd-8254-4680-8aa2-8fc3aa46cb16"),
            season20222023,
            name: "Ere")
        {
            FrameCount = 18,
            MinPlayerClass = 1,
            RoundsDuringSeason = 2,
            SortOrder = 1
        };

        Team teamBiljartLoungeA = new Team(
            id: Guid.Parse("d7fe88a4-7384-4000-a78b-5f7ae68aa077"),
            divisionEre,
            clubBiljartLounge,
            name: "A");
        divisionEre.Teams.Add(teamBiljartLoungeA);
        clubBiljartLounge.Teams.Add(teamBiljartLoungeA);

        Team teamBuckinghamA = new Team(
            id: Guid.Parse("31d19b88-4a6d-4d4a-b1e8-46e64b371adb"),
            divisionEre,
            clubBuckingham,
            name: "A");
        divisionEre.Teams.Add(teamBuckinghamA);
        clubBuckingham.Teams.Add(teamBuckinghamA);

        Team teamBuckinghamB = new Team(
            id: Guid.Parse("81bea352-4ede-4332-be03-9e2e8fb17180"),
            divisionEre,
            clubBuckingham,
            name: "B");
        divisionEre.Teams.Add(teamBuckinghamB);
        clubBuckingham.Teams.Add(teamBuckinghamB);

        Team teamBuckinghamC = new Team(
            id: Guid.Parse("707068fb-1321-4ef5-853a-c1625172d3b4"),
            divisionEre,
            clubBuckingham,
            name: "C");
        divisionEre.Teams.Add(teamBuckinghamC);
        clubBuckingham.Teams.Add(teamBuckinghamC);

        Team teamDeKreeftA = new Team(
            id: Guid.Parse("0d470757-1855-47a4-9279-fc625ecfe09b"),
            divisionEre,
            clubDeKreeft,
            name: "A");
        divisionEre.Teams.Add(teamDeKreeftA);
        clubDeKreeft.Teams.Add(teamDeKreeftA);

        Team teamHappySnookerA = new Team(
            id: Guid.Parse("f0f59050-c523-453b-9642-d80c47819e79"),
            divisionEre,
            clubHappySnooker,
            name: "A");
        divisionEre.Teams.Add(teamHappySnookerA);
        clubHappySnooker.Teams.Add(teamHappySnookerA);

        Team teamHappySnookerB = new Team(
            id: Guid.Parse("e85b6c5f-c4aa-4132-b20b-80c3dd13af01"),
            divisionEre,
            clubHappySnooker,
            name: "B");
        divisionEre.Teams.Add(teamHappySnookerB);
        clubHappySnooker.Teams.Add(teamHappySnookerB);

        Team teamHappySnookerC = new Team(
            id: Guid.Parse("e6ea0f92-3c96-47d6-a8d8-57b802996555"),
            divisionEre,
            clubHappySnooker,
            name: "C");
        divisionEre.Teams.Add(teamHappySnookerC);
        clubHappySnooker.Teams.Add(teamHappySnookerC);

        Team teamReSpotA = new Team(
            id: Guid.Parse("d1894dbe-ad35-4919-af1b-ce52af2a5a89"),
            divisionEre,
            clubReSpot,
            name: "A");
        divisionEre.Teams.Add(teamReSpotA);
        clubReSpot.Teams.Add(teamReSpotA);

        Team teamReSpotB = new Team(
            id: Guid.Parse("e97a78db-b929-45c5-b263-1184b5e47f8a"),
            divisionEre,
            clubReSpot,
            name: "B");
        divisionEre.Teams.Add(teamReSpotB);
        clubReSpot.Teams.Add(teamReSpotB);

        Team teamReSpotC = new Team(
            id: Guid.Parse("a7476bd5-f28d-4b6b-88f4-8c60de8c9c77"),
            divisionEre,
            clubReSpot,
            name: "C");
        divisionEre.Teams.Add(teamReSpotC);
        clubReSpot.Teams.Add(teamReSpotC);

        Team teamRileyInnA = new Team(
            id: Guid.Parse("b173ff74-86d9-48c0-9a7a-8e3e783509c9"),
            divisionEre,
            clubRileyInn,
            name: "A");
        divisionEre.Teams.Add(teamRileyInnA);
        clubRileyInn.Teams.Add(teamRileyInnA);

        Team teamRileyInnB = new Team(
            id: Guid.Parse("784989e3-a000-4b60-a8ca-5fccde978fb5"),
            divisionEre,
            clubRileyInn,
            name: "B");
        divisionEre.Teams.Add(teamRileyInnB);
        clubRileyInn.Teams.Add(teamRileyInnB);

        Team teamRileyInnC = new Team(
            id: Guid.Parse("807ca2e4-cbfd-4a76-8b93-e1a2889b8568"),
            divisionEre,
            clubRileyInn,
            name: "C");
        divisionEre.Teams.Add(teamRileyInnC);
        clubRileyInn.Teams.Add(teamRileyInnC);

        Team teamZumaA = new Team(
            id: Guid.Parse("62c25faa-88d1-453a-863b-c11e22c852ab"),
            divisionEre,
            clubZuma,
            name: "A");
        divisionEre.Teams.Add(teamZumaA);
        clubZuma.Teams.Add(teamZumaA);

        Team teamZumaB = new Team(
            id: Guid.Parse("34f1146c-ac89-41ac-800a-86bee0e44957"),
            divisionEre,
            clubZuma,
            name: "B");
        divisionEre.Teams.Add(teamZumaB);
        clubZuma.Teams.Add(teamZumaB);

        season20222023.Divisions.Add(divisionEre);

        Division division1 = new Division(
            id: Guid.Parse("47a1a8b5-7845-4767-be7c-ed5b13af2ca7"),
            season20222023,
            name: "1st")
        {
            FrameCount = 18,
            MinPlayerClass = 1,
            RoundsDuringSeason = 2,
            SortOrder = 2
        };

        Team teamBiljartLoungeB = new Team(
            id: Guid.Parse("22893950-166a-4498-ad2c-6a2b5552652c"),
            division1,
            clubBiljartLounge,
            name: "B");
        division1.Teams.Add(teamBiljartLoungeB);
        clubBiljartLounge.Teams.Add(teamBiljartLoungeB);

        Team teamBiljartLoungeC = new Team(
            id: Guid.Parse("77e99c8d-c6d1-46a3-9e2f-f3fe691c9307"),
            division1,
            clubBiljartLounge,
            name: "C");
        division1.Teams.Add(teamBiljartLoungeC);
        clubBiljartLounge.Teams.Add(teamBiljartLoungeC);

        Team teamBuckinghamD = new Team(
            id: Guid.Parse("fb2ea7a1-539e-4a31-9cae-2e685d8abcd6"),
            division1,
            clubBuckingham,
            name: "D");
        division1.Teams.Add(teamBuckinghamD);
        clubBuckingham.Teams.Add(teamBuckinghamD);

        Team teamBuckinghamE = new Team(
            id: Guid.Parse("d81d6d6f-9b42-4833-a876-65e4c0b377c5"),
            division1,
            clubBuckingham,
            name: "E");
        division1.Teams.Add(teamBuckinghamE);
        clubBuckingham.Teams.Add(teamBuckinghamE);

        Team teamDeKreeftB = new Team(
            id: Guid.Parse("62579558-8e96-4c3a-9340-91239561fcf3"),
            division1,
            clubDeKreeft,
            name: "B");
        division1.Teams.Add(teamDeKreeftB);
        clubDeKreeft.Teams.Add(teamDeKreeftB);

        Team teamDeKreeftC = new Team(
            id: Guid.Parse("eda0ae2a-d04b-4632-94fc-f7d2f895d882"),
            division1,
            clubDeKreeft,
            name: "C");
        division1.Teams.Add(teamDeKreeftC);
        clubDeKreeft.Teams.Add(teamDeKreeftC);

        Team teamDeKreeftD = new Team(
            id: Guid.Parse("093ffcfe-b1a6-4c38-8d24-069dc83d8936"),
            division1,
            clubDeKreeft,
            name: "D");
        division1.Teams.Add(teamDeKreeftD);
        clubDeKreeft.Teams.Add(teamDeKreeftD);

        Team teamDeMaxxA = new Team(
            id: Guid.Parse("a515eded-f7a9-403a-8b38-f1025742f54e"),
            division1,
            clubDeMaxx,
            name: "A");
        division1.Teams.Add(teamDeMaxxA);
        clubDeMaxx.Teams.Add(teamDeMaxxA);

        Team teamDeMaxxB = new Team(
            id: Guid.Parse("00bdad7b-437d-4e94-9ec6-52210499f092"),
            division1,
            clubDeMaxx,
            name: "B");
        division1.Teams.Add(teamDeMaxxB);
        clubDeMaxx.Teams.Add(teamDeMaxxB);

        Team teamDeMaxxC = new Team(
            id: Guid.Parse("6cb97fa9-c66e-4d85-9d3e-9153e274f700"),
            division1,
            clubDeMaxx,
            name: "C");
        division1.Teams.Add(teamDeMaxxC);
        clubDeMaxx.Teams.Add(teamDeMaxxC);

        Team teamHappySnookerD = new Team(
            id: Guid.Parse("9d3969cc-aa81-48dd-8180-d44b070be7b4"),
            division1,
            clubHappySnooker,
            name: "D");
        division1.Teams.Add(teamHappySnookerD);
        clubHappySnooker.Teams.Add(teamHappySnookerD);

        Team teamNRGA = new Team(
            id: Guid.Parse("234f1401-6b84-4e7b-8067-4885502ee4e3"),
            division1,
            clubNRG,
            name: "A");
        division1.Teams.Add(teamNRGA);
        clubNRG.Teams.Add(teamNRGA);

        Team teamNRGB = new Team(
            id: Guid.Parse("23b5c5e0-5d90-4826-aa75-5b0d7234fe4c"),
            division1,
            clubNRG,
            name: "B");
        division1.Teams.Add(teamNRGB);
        clubNRG.Teams.Add(teamNRGB);

        Team teamReSpotD = new Team(
            id: Guid.Parse("d3123654-e197-4d71-b5d5-e03ae605a4a7"),
            division1,
            clubReSpot,
            name: "D");
        division1.Teams.Add(teamReSpotD);
        clubReSpot.Teams.Add(teamReSpotD);

        Team teamReSpotE = new Team(
            id: Guid.Parse("71d11e3c-8dee-4789-9aa5-e1e5bae13afd"),
            division1,
            clubReSpot,
            name: "E");
        division1.Teams.Add(teamReSpotE);
        clubReSpot.Teams.Add(teamReSpotE);

        Team teamRileyInnD = new Team(
            id: Guid.Parse("c4bc8b44-eb52-43a4-a11f-21bd0c5c9cb6"),
            division1,
            clubRileyInn,
            name: "D");
        division1.Teams.Add(teamRileyInnD);
        clubRileyInn.Teams.Add(teamRileyInnD);

        Team teamSnookerSportsA = new Team(
            id: Guid.Parse("a482feeb-deba-48c4-8610-fd7f309414a8"),
            division1,
            clubSnookerSports,
            name: "A");
        division1.Teams.Add(teamSnookerSportsA);
        clubSnookerSports.Teams.Add(teamSnookerSportsA);

        season20222023.Divisions.Add(division1);

        Division division2 = new Division(
            id: Guid.Parse("0842c4cf-4579-4f01-b2e7-0c90141ffee8"),
            season20222023,
            name: "2nd")
        {
            FrameCount = 18,
            MinPlayerClass = 1,
            RoundsDuringSeason = 2,
            SortOrder = 3
        };

        Team teamBuckinghamF = new Team(
            id: Guid.Parse("3295b72f-29f8-423d-a0f0-150775b843da"),
            division2,
            clubBuckingham,
            name: "F");
        division2.Teams.Add(teamBuckinghamF);
        clubBuckingham.Teams.Add(teamBuckinghamF);

        Team teamBuckinghamG = new Team(
            id: Guid.Parse("518fe22b-d019-4954-bbbf-33ee802dd78c"),
            division2,
            clubBuckingham,
            name: "G");
        division2.Teams.Add(teamBuckinghamG);
        clubBuckingham.Teams.Add(teamBuckinghamG);

        Team teamDeKreeftE = new Team(
            id: Guid.Parse("997a6809-6e65-4ca4-a8df-1543f46daa8e"),
            division2,
            clubDeKreeft,
            name: "E");
        division2.Teams.Add(teamDeKreeftE);
        clubDeKreeft.Teams.Add(teamDeKreeftE);

        Team teamDeKreeftF = new Team(
            id: Guid.Parse("20e462e3-511c-451c-8e5a-36ffe4f1764f"),
            division2,
            clubDeKreeft,
            name: "F");
        division2.Teams.Add(teamDeKreeftF);
        clubDeKreeft.Teams.Add(teamDeKreeftF);

        Team teamHappySnookerE = new Team(
            id: Guid.Parse("f78e6613-a406-4a6b-b29c-2c828547bea8"),
            division2,
            clubHappySnooker,
            name: "E");
        division2.Teams.Add(teamHappySnookerE);
        clubHappySnooker.Teams.Add(teamHappySnookerE);

        Team teamHappySnookerF = new Team(
            id: Guid.Parse("4adcfd11-2e54-4154-a602-3972ce662302"),
            division2,
            clubHappySnooker,
            name: "F");
        division2.Teams.Add(teamHappySnookerF);
        clubHappySnooker.Teams.Add(teamHappySnookerF);

        Team teamHappySnookerG = new Team(
            id: Guid.Parse("2a9f87e8-9d79-4183-a17e-3cca41e23ec2"),
            division2,
            clubHappySnooker,
            name: "G");
        division2.Teams.Add(teamHappySnookerG);
        clubHappySnooker.Teams.Add(teamHappySnookerG);

        Team teamNRGC = new Team(
            id: Guid.Parse("3b84dceb-2c48-4380-b603-ee00172dfd63"),
            division2,
            clubNRG,
            name: "C");
        division2.Teams.Add(teamNRGC);
        clubNRG.Teams.Add(teamNRGC);

        Team teamReSpotF = new Team(
            id: Guid.Parse("ca922235-b92d-478f-a360-e9e98932f0cf"),
            division2,
            clubReSpot,
            name: "F");
        division2.Teams.Add(teamReSpotF);
        clubReSpot.Teams.Add(teamReSpotF);

        Team teamReSpotG = new Team(
            id: Guid.Parse("11185623-6a3f-486d-983b-ca931922723d"),
            division2,
            clubReSpot,
            name: "G");
        division2.Teams.Add(teamReSpotG);
        clubReSpot.Teams.Add(teamReSpotG);

        Team teamRileyInnE = new Team(
            id: Guid.Parse("1d8bf0b3-2870-47fa-a7c8-66faaf03d974"),
            division2,
            clubRileyInn,
            name: "E");
        division2.Teams.Add(teamRileyInnE);
        clubRileyInn.Teams.Add(teamRileyInnE);

        Team teamRileyInnF = new Team(
            id: Guid.Parse("43674326-c64a-450c-a717-84580e68eb55"),
            division2,
            clubRileyInn,
            name: "F");
        division2.Teams.Add(teamRileyInnF);
        clubRileyInn.Teams.Add(teamRileyInnF);

        Team teamRileyInnG = new Team(
            id: Guid.Parse("1127b903-780c-43ce-bfc5-433d29e15385"),
            division2,
            clubRileyInn,
            name: "G");
        division2.Teams.Add(teamRileyInnG);
        clubRileyInn.Teams.Add(teamRileyInnG);

        Team teamSnookerSportsB = new Team(
            id: Guid.Parse("885c556a-5164-45c9-8a03-8dc8a78f3ab7"),
            division2,
            clubSnookerSports,
            name: "B");
        division2.Teams.Add(teamSnookerSportsB);
        clubSnookerSports.Teams.Add(teamSnookerSportsB);

        Team teamZumaC = new Team(
            id: Guid.Parse("c572dc0a-f48c-461a-974e-71797a1056d0"),
            division2,
            clubZuma,
            name: "C");
        division2.Teams.Add(teamZumaC);
        clubZuma.Teams.Add(teamZumaC);

        Team teamZumaD = new Team(
            id: Guid.Parse("b8d2b913-5d69-46cd-9c55-71620fd9f04f"),
            division2,
            clubZuma,
            name: "D");
        division2.Teams.Add(teamZumaD);
        clubZuma.Teams.Add(teamZumaD);

        season20222023.Divisions.Add(division2);

        Division division3 = new Division(
            id: Guid.Parse("c6e35a27-f9ae-4506-830f-75b7d01a9fa2"),
            season20222023,
            name: "3rd")
        {
            FrameCount = 12,
            MinPlayerClass = 2,
            RoundsDuringSeason = 2,
            SortOrder = 4
        };

        Team teamDeKreeftG = new Team(
            id: Guid.Parse("5ca67816-acbd-4baf-87cf-a2392319aea2"),
            division3,
            clubDeKreeft,
            name: "G");
        division3.Teams.Add(teamDeKreeftG);
        clubDeKreeft.Teams.Add(teamDeKreeftG);

        Team teamDeKreeftH = new Team(
            id: Guid.Parse("235ddb9f-1d7c-4db9-a0eb-a84ceb8a9676"),
            division3,
            clubDeKreeft,
            name: "H");
        division3.Teams.Add(teamDeKreeftH);
        clubDeKreeft.Teams.Add(teamDeKreeftH);

        Team teamDeKreeftI = new Team(
            id: Guid.Parse("5779e83a-2158-4fa9-b27b-3aff749bba5b"),
            division3,
            clubDeKreeft,
            name: "I");
        division3.Teams.Add(teamDeKreeftI);
        clubDeKreeft.Teams.Add(teamDeKreeftI);

        Team teamDeMaxxD = new Team(
            id: Guid.Parse("b3333ca0-2229-4243-83b8-1351c24d9f4f"),
            division3,
            clubDeMaxx,
            name: "D");
        division3.Teams.Add(teamDeMaxxD);
        clubDeMaxx.Teams.Add(teamDeMaxxD);

        Team teamDeMaxxE = new Team(
            id: Guid.Parse("906698b0-7b84-4065-a9ee-b217011501d8"),
            division3,
            clubDeMaxx,
            name: "E");
        division3.Teams.Add(teamDeMaxxE);
        clubDeMaxx.Teams.Add(teamDeMaxxE);

        Team teamHappySnookerH = new Team(
            id: Guid.Parse("ee97e9a3-4c0d-4c5c-9d3e-00b4f90d1c9e"),
            division3,
            clubHappySnooker,
            name: "H");
        division3.Teams.Add(teamHappySnookerH);
        clubHappySnooker.Teams.Add(teamHappySnookerH);

        Team teamHappySnookerI = new Team(
            id: Guid.Parse("0b5c1fd8-32ee-4b46-ba41-3491e1f2e573"),
            division3,
            clubHappySnooker,
            name: "I");
        division3.Teams.Add(teamHappySnookerI);
        clubHappySnooker.Teams.Add(teamHappySnookerI);

        Team teamHappySnookerJ = new Team(
            id: Guid.Parse("3eb42383-6550-4f57-8225-35c0b27600eb"),
            division3,
            clubHappySnooker,
            name: "J");
        division3.Teams.Add(teamHappySnookerJ);
        clubHappySnooker.Teams.Add(teamHappySnookerJ);

        Team teamHappySnookerK = new Team(
            id: Guid.Parse("499804ab-eaf6-4365-9006-1a0d88a38c90"),
            division3,
            clubHappySnooker,
            name: "K");
        division3.Teams.Add(teamHappySnookerK);
        clubHappySnooker.Teams.Add(teamHappySnookerK);

        Team teamNRGD = new Team(
            id: Guid.Parse("46173b28-8de1-4bb7-b3f1-1e914873e69b"),
            division3,
            clubNRG,
            name: "D");
        division3.Teams.Add(teamNRGD);
        clubNRG.Teams.Add(teamNRGD);

        Team teamReSpotH = new Team(
            id: Guid.Parse("f906e77c-fb4c-4c62-bf40-9fa018a99484"),
            division3,
            clubReSpot,
            name: "H");
        division3.Teams.Add(teamReSpotH);
        clubReSpot.Teams.Add(teamReSpotH);

        Team teamReSpotI = new Team(
            id: Guid.Parse("745cc76e-cb2b-4786-b100-fc542b9d3a90"),
            division3,
            clubReSpot,
            name: "I");
        division3.Teams.Add(teamReSpotI);
        clubReSpot.Teams.Add(teamReSpotI);

        Team teamRileyInnH = new Team(
            id: Guid.Parse("0bf54219-21e6-4d48-8bcc-a7a6259b819d"),
            division3,
            clubRileyInn,
            name: "H");
        division3.Teams.Add(teamRileyInnH);
        clubRileyInn.Teams.Add(teamRileyInnH);

        Team teamRileyInnI = new Team(
            id: Guid.Parse("58d5d14e-ab8f-4100-9dfb-e96a6bb9c777"),
            division3,
            clubRileyInn,
            name: "I");
        division3.Teams.Add(teamRileyInnI);
        clubRileyInn.Teams.Add(teamRileyInnI);

        Team teamRileyInnJ = new Team(
            id: Guid.Parse("7085fe91-e9a9-43ad-b9f8-d94e75140b17"),
            division3,
            clubRileyInn,
            name: "J");
        division3.Teams.Add(teamRileyInnJ);
        clubRileyInn.Teams.Add(teamRileyInnJ);

        Team teamZumaE = new Team(
            id: Guid.Parse("12b1c133-072c-4ad4-9772-a3881ecf75ba"),
            division3,
            clubZuma,
            name: "E");
        division3.Teams.Add(teamZumaE);
        clubZuma.Teams.Add(teamZumaE);

        Team teamZumaF = new Team(
            id: Guid.Parse("e54060ee-393e-4d04-847f-cbfc1c2a77fb"),
            division3,
            clubZuma,
            name: "F");
        division3.Teams.Add(teamZumaF);
        clubZuma.Teams.Add(teamZumaF);

        season20222023.Divisions.Add(division3);

        Division division4 = new Division(
            id: Guid.Parse("4ce1630d-fd5c-42cb-b6c4-3100a1d32d60"),
            season20222023,
            name: "4th")
        {
            FrameCount = 12,
            MinPlayerClass = 2,
            RoundsDuringSeason = 2,
            SortOrder = 5
        };

        Team teamBiljartLoungeD = new Team(
            id: Guid.Parse("71ba69d5-425e-469a-820c-c87f06d64ebc"),
            division4,
            clubBiljartLounge,
            name: "D");
        division4.Teams.Add(teamBiljartLoungeD);
        clubBiljartLounge.Teams.Add(teamBiljartLoungeD);

        Team teamBuckinghamH = new Team(
            id: Guid.Parse("e542d41e-c1dc-4f0e-a0e1-918738a36dff"),
            division4,
            clubBuckingham,
            name: "H");
        division4.Teams.Add(teamBuckinghamH);
        clubBuckingham.Teams.Add(teamBuckinghamH);

        Team teamBuckinghamI = new Team(
            id: Guid.Parse("793a9dcf-181c-4701-9dc5-f32d4b9b602f"),
            division4,
            clubBuckingham,
            name: "I");
        division4.Teams.Add(teamBuckinghamI);
        clubBuckingham.Teams.Add(teamBuckinghamI);

        Team teamDeKreeftJ = new Team(
            id: Guid.Parse("817b3dfa-de29-49c3-974b-7fe1916ef43e"),
            division4,
            clubDeKreeft,
            name: "J");
        division4.Teams.Add(teamDeKreeftJ);
        clubDeKreeft.Teams.Add(teamDeKreeftJ);

        Team teamDeKreeftK = new Team(
            id: Guid.Parse("feb1bd1a-d95a-47b7-8ba0-678a8a16b896"),
            division4,
            clubDeKreeft,
            name: "K");
        division4.Teams.Add(teamDeKreeftK);
        clubDeKreeft.Teams.Add(teamDeKreeftK);

        Team teamDeKreeftL = new Team(
            id: Guid.Parse("e3e3b8ed-1937-4aef-baaf-da46642b56b6"),
            division4,
            clubDeKreeft,
            name: "L");
        division4.Teams.Add(teamDeKreeftL);
        clubDeKreeft.Teams.Add(teamDeKreeftL);

        Team teamDeMaxxF = new Team(
            id: Guid.Parse("9e488b11-634f-4261-996c-f3e8bf741e1e"),
            division4,
            clubDeMaxx,
            name: "F");
        division4.Teams.Add(teamDeMaxxF);
        clubDeMaxx.Teams.Add(teamDeMaxxF);

        Team teamHappySnookerL = new Team(
            id: Guid.Parse("ed42e114-9635-4b83-bc29-0b868c16f2be"),
            division4,
            clubHappySnooker,
            name: "L");
        division4.Teams.Add(teamHappySnookerL);
        clubHappySnooker.Teams.Add(teamHappySnookerL);

        Team teamHappySnookerM = new Team(
            id: Guid.Parse("4d827846-949f-43f0-b09d-ec5f2eae3bf1"),
            division4,
            clubHappySnooker,
            name: "M");
        division4.Teams.Add(teamHappySnookerM);
        clubHappySnooker.Teams.Add(teamHappySnookerM);

        Team teamHappySnookerN = new Team(
            id: Guid.Parse("198dc1ed-1728-4d08-ab30-55b8a09ab681"),
            division4,
            clubHappySnooker,
            name: "N");
        division4.Teams.Add(teamHappySnookerN);
        clubHappySnooker.Teams.Add(teamHappySnookerN);

        Team teamNRGE = new Team(
            id: Guid.Parse("4dc189af-9e95-4ea9-97f5-0fd256a8d4dd"),
            division4,
            clubNRG,
            name: "E");
        division4.Teams.Add(teamNRGE);
        clubNRG.Teams.Add(teamNRGE);

        Team teamReSpotJ = new Team(
            id: Guid.Parse("6f3cfc7c-45d1-4f91-b1f1-35855962c2e3"),
            division4,
            clubReSpot,
            name: "J");
        division4.Teams.Add(teamReSpotJ);
        clubReSpot.Teams.Add(teamReSpotJ);

        Team teamReSpotK = new Team(
            id: Guid.Parse("109e16f2-d7a4-4c74-ab29-8ade03dd309f"),
            division4,
            clubReSpot,
            name: "K");
        division4.Teams.Add(teamReSpotK);
        clubReSpot.Teams.Add(teamReSpotK);

        Team teamReSpotL = new Team(
            id: Guid.Parse("1547f011-c538-4d6d-a7d1-34bf7c9a2ec7"),
            division4,
            clubReSpot,
            name: "L");
        division4.Teams.Add(teamReSpotL);
        clubReSpot.Teams.Add(teamReSpotL);

        Team teamReSpotM = new Team(
            id: Guid.Parse("74a169e7-4ace-46fd-8315-7ab4a6d4b03a"),
            division4,
            clubReSpot,
            name: "M");
        division4.Teams.Add(teamReSpotM);
        clubReSpot.Teams.Add(teamReSpotM);

        Team teamRileyInnK = new Team(
            id: Guid.Parse("d45c8164-7b5a-4d4c-b563-da96d486e701"),
            division4,
            clubRileyInn,
            name: "K");
        division4.Teams.Add(teamRileyInnK);
        clubRileyInn.Teams.Add(teamRileyInnK);

        Team teamZumaG = new Team(
            id: Guid.Parse("e4023a8f-4076-4e65-a1c4-49d2cd40762e"),
            division4,
            clubZuma,
            name: "G");
        division4.Teams.Add(teamZumaG);
        clubZuma.Teams.Add(teamZumaG);

        season20222023.Divisions.Add(division4);

        Division division5 = new Division(
            id: Guid.Parse("953a5174-55b4-4e81-973a-a6d25aa1195b"),
            season20222023,
            name: "5th")
        {
            FrameCount = 9,
            MinPlayerClass = 3,
            RoundsDuringSeason = 2,
            SortOrder = 6
        };

        Team teamBiljartLoungeE = new Team(
            id: Guid.Parse("608c0463-e1d2-4fb4-8ba0-9754b3611e05"),
            division5,
            clubBiljartLounge,
            name: "E");
        division5.Teams.Add(teamBiljartLoungeE);
        clubBiljartLounge.Teams.Add(teamBiljartLoungeE);

        Team teamHappySnookerO = new Team(
            id: Guid.Parse("2f795fe6-ea66-4916-84f3-8da53925b057"),
            division5,
            clubHappySnooker,
            name: "O");
        division5.Teams.Add(teamHappySnookerO);
        clubHappySnooker.Teams.Add(teamHappySnookerO);

        Team teamHappySnookerP = new Team(
            id: Guid.Parse("aa1065eb-e76d-4c75-bbc0-caab962c55ed"),
            division5,
            clubHappySnooker,
            name: "P");
        division5.Teams.Add(teamHappySnookerP);
        clubHappySnooker.Teams.Add(teamHappySnookerP);

        Team teamHappySnookerQ = new Team(
            id: Guid.Parse("d4485aa8-d5a6-48f3-ba5f-85d96523e0ae"),
            division5,
            clubHappySnooker,
            name: "Q");
        division5.Teams.Add(teamHappySnookerQ);
        clubHappySnooker.Teams.Add(teamHappySnookerQ);

        Team teamReSpotN = new Team(
            id: Guid.Parse("fdbaaa0f-b4ed-4a14-ab74-911c60c4a956"),
            division5,
            clubReSpot,
            name: "N");
        division5.Teams.Add(teamReSpotN);
        clubReSpot.Teams.Add(teamReSpotN);

        Team teamReSpotO = new Team(
            id: Guid.Parse("3681ed12-14f1-490b-a2f9-336cc99bb824"),
            division5,
            clubReSpot,
            name: "O");
        division5.Teams.Add(teamReSpotO);
        clubReSpot.Teams.Add(teamReSpotO);

        Team teamReSpotP = new Team(
            id: Guid.Parse("2f07d12d-844a-4409-b3f7-f9a4ce09e511"),
            division5,
            clubReSpot,
            name: "P");
        division5.Teams.Add(teamReSpotP);
        clubReSpot.Teams.Add(teamReSpotP);

        Team teamReSpotQ = new Team(
            id: Guid.Parse("e87a4ce1-cd10-498e-b7e4-114afb1ac811"),
            division5,
            clubReSpot,
            name: "Q");
        division5.Teams.Add(teamReSpotQ);
        clubReSpot.Teams.Add(teamReSpotQ);

        Team teamReSpotR = new Team(
            id: Guid.Parse("4497b8cb-b3cd-41a7-8a3f-ea8f30f82ce5"),
            division5,
            clubReSpot,
            name: "R");
        division5.Teams.Add(teamReSpotR);
        clubReSpot.Teams.Add(teamReSpotR);

        Team teamReSpotS = new Team(
            id: Guid.Parse("8f6330f5-0dcf-4a3f-9fbf-a362a1a2710c"),
            division5,
            clubReSpot,
            name: "S");
        division5.Teams.Add(teamReSpotS);
        clubReSpot.Teams.Add(teamReSpotS);

        Team teamReSpotT = new Team(
            id: Guid.Parse("adaef4b2-6c3f-4370-b735-cb88b6c06b4e"),
            division5,
            clubReSpot,
            name: "T");
        division5.Teams.Add(teamReSpotT);
        clubReSpot.Teams.Add(teamReSpotT);

        Team teamRileyInnL = new Team(
            id: Guid.Parse("38100d49-c486-4df8-a6c2-fb7bd253c40f"),
            division5,
            clubRileyInn,
            name: "L");
        division5.Teams.Add(teamRileyInnL);
        clubRileyInn.Teams.Add(teamRileyInnL);

        Team teamRileyInnM = new Team(
            id: Guid.Parse("49716688-88bb-494d-9c3b-b23fe077279a"),
            division5,
            clubRileyInn,
            name: "M");
        division5.Teams.Add(teamRileyInnM);
        clubRileyInn.Teams.Add(teamRileyInnM);

        Team teamRileyInnN = new Team(
            id: Guid.Parse("7b92d520-f44f-41dd-aa44-f474ed664019"),
            division5,
            clubRileyInn,
            name: "N");
        division5.Teams.Add(teamRileyInnN);
        clubRileyInn.Teams.Add(teamRileyInnN);

        Team teamZumaH = new Team(
            id: Guid.Parse("22ffc8b1-138e-40c8-b6eb-6e65610c2e6c"),
            division5,
            clubZuma,
            name: "H");
        division5.Teams.Add(teamZumaH);
        clubZuma.Teams.Add(teamZumaH);

        Team teamZumaI = new Team(
            id: Guid.Parse("0101caec-72d8-4c60-a3b5-1f36f94a3776"),
            division5,
            clubZuma,
            name: "I");
        division5.Teams.Add(teamZumaI);
        clubZuma.Teams.Add(teamZumaI);

        season20222023.Divisions.Add(division5);

        Division divisionSaturday = new Division(
            id: Guid.Parse("65ec4fde-a054-4816-8244-5e0d149751a4"),
            season20222023,
            name: "Saturday")
        {
            FrameCount = 12,
            MinPlayerClass = 3,
            RoundsDuringSeason = 4,
            SortOrder = 7,
            DaysOfWeek = new List<DayOfWeek>()
            {
                DayOfWeek.Saturday
            }
        };

        Team teamDeKreeftM = new Team(
            id: Guid.Parse("311f0d33-a3b0-43fb-b789-b1c359afc8d0"),
            divisionSaturday,
            clubDeKreeft,
            name: "M");
        divisionSaturday.Teams.Add(teamDeKreeftM);
        clubDeKreeft.Teams.Add(teamDeKreeftM);

        Team teamHappySnookerR = new Team(
            id: Guid.Parse("194273d5-75f6-4b67-a9ab-1277f4f73aa3"),
            divisionSaturday,
            clubHappySnooker,
            name: "R");
        divisionSaturday.Teams.Add(teamHappySnookerR);
        clubHappySnooker.Teams.Add(teamHappySnookerR);

        Team teamNRGF = new Team(
            id: Guid.Parse("275ae8d7-57ea-4a63-b12c-db38fabd71f9"),
            divisionSaturday,
            clubNRG,
            name: "F");
        divisionSaturday.Teams.Add(teamNRGF);
        clubNRG.Teams.Add(teamNRGF);

        Team teamReSpotU = new Team(
            id: Guid.Parse("a54c1ec4-0f58-49f0-b7d8-6c925e15a6ae"),
            divisionSaturday,
            clubReSpot,
            name: "U");
        divisionSaturday.Teams.Add(teamReSpotU);
        clubReSpot.Teams.Add(teamReSpotU);

        Team teamReSpotV = new Team(
            id: Guid.Parse("01e3a70d-b3fa-4fff-9f2f-8a4fbe6fb79b"),
            divisionSaturday,
            clubReSpot,
            name: "V");
        divisionSaturday.Teams.Add(teamReSpotV);
        clubReSpot.Teams.Add(teamReSpotV);

        Team teamReSpotW = new Team(
            id: Guid.Parse("fa3606a0-682b-47b7-8113-0933c7d3b07e"),
            divisionSaturday,
            clubReSpot,
            name: "W");
        divisionSaturday.Teams.Add(teamReSpotW);
        clubReSpot.Teams.Add(teamReSpotW);

        Team teamReSpotX = new Team(
            id: Guid.Parse("b22e4276-3c72-4c62-9458-8f2a1a1ca47e"),
            divisionSaturday,
            clubReSpot,
            name: "X");
        divisionSaturday.Teams.Add(teamReSpotX);
        clubReSpot.Teams.Add(teamReSpotX);

        season20222023.Divisions.Add(divisionSaturday);

        await _seasonRepository.InsertAsync(season20222023);
    }
}