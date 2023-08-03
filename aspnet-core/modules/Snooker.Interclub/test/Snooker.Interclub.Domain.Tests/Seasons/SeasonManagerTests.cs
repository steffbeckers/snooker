using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Teams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Timing;
using Xunit;

namespace Snooker.Interclub.Seasons;

public class SeasonManagerTests : InterclubDomainTestBase
{
    private readonly IClock _clock;
    private readonly ClubManager _clubManager;
    private readonly SeasonManager _seasonManager;

    public SeasonManagerTests()
    {
        _clock = GetRequiredService<IClock>();
        _clubManager = GetRequiredService<ClubManager>();
        _seasonManager = GetRequiredService<SeasonManager>();
    }

    [Fact]
    public async Task Should_Schedule()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            // Arrange
            Season season = new Season(
                id: Guid.Parse("1877aa91-3906-48f0-a4eb-eb7dc9358835"),
                startDate: new DateTime(_clock.Now.Year, 1, 1),
                endDate: new DateTime(_clock.Now.AddYears(1).Year, 1, 1));

            Club clubBiljartLounge = await _clubManager.CreateAsync(
                id: Guid.Parse("d1610828-8df5-4ced-8bf3-d876f6207691"),
                name: "Biljart Lounge");
            clubBiljartLounge.NumberOfTables = 4;

            Club clubBuckingham = await _clubManager.CreateAsync(
                id: Guid.Parse("1252eaad-0efa-433f-a598-4909158de769"),
                name: "Buckingham");
            clubBuckingham.NumberOfTables = 8;

            Club clubDeKreeft = await _clubManager.CreateAsync(
                id: Guid.Parse("c95db223-c2c3-49e6-b0cd-d823327d1814"),
                name: "De Kreeft");
            clubDeKreeft.NumberOfTables = 6;

            Club clubDeMaxx = await _clubManager.CreateAsync(
                id: Guid.Parse("a0d1964b-92c1-42dd-a5a9-a45a21f1279c"),
                name: "De Maxx");
            clubDeMaxx.NumberOfTables = 10;

            Club clubHappySnooker = await _clubManager.CreateAsync(
                id: Guid.Parse("9a03e899-1054-4e48-86e3-ff7250d48eb5"),
                name: "Happy Snooker");
            clubHappySnooker.NumberOfTables = 5;

            Club clubNRG = await _clubManager.CreateAsync(
                id: Guid.Parse("d72092b9-1476-4954-914a-2bfb1c8c73e6"),
                name: "NRG");
            clubNRG.NumberOfTables = 3;

            Club clubReSpot = await _clubManager.CreateAsync(
                id: Guid.Parse("e6071606-563e-4e73-beb6-962dfe3df769"),
                name: "Re-Spot");
            clubReSpot.NumberOfTables = 6;

            Club clubRileyInn = await _clubManager.CreateAsync(
                id: Guid.Parse("19d70aac-ba91-4a53-84b4-69cf4e694f0a"),
                name: "Riley Inn");
            clubRileyInn.NumberOfTables = 8;

            Club clubSnookerSports = await _clubManager.CreateAsync(
                id: Guid.Parse("95adc0ff-e6ab-4d52-aa64-82f4849228fc"),
                name: "Snooker Sports");
            clubSnookerSports.NumberOfTables = 3;

            Club clubZuma = await _clubManager.CreateAsync(
                id: Guid.Parse("e8fb5f9d-b5a5-4456-83bf-43282490e87b"),
                name: "Zuma");
            clubZuma.NumberOfTables = 6;

            Division divisionEre = new Division(
                id: Guid.Parse("3c7923fd-8254-4680-8aa2-8fc3aa46cb16"),
                season,
                name: "Ere")
            {
                FrameCount = 18,
                MinPlayerClass = 1,
                RoundsPerSeasonCount = 2,
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
                id: Guid.Parse("22893950-166a-4498-ad2c-6a2b5552652c"),
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

            season.Divisions.Add(divisionEre);

            Division division1 = new Division(
                id: Guid.Parse("47a1a8b5-7845-4767-be7c-ed5b13af2ca7"),
                season,
                name: "1st")
            {
                FrameCount = 18,
                MinPlayerClass = 1,
                RoundsPerSeasonCount = 2,
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

            season.Divisions.Add(division1);

            Division division2 = new Division(
                id: Guid.Parse("0842c4cf-4579-4f01-b2e7-0c90141ffee8"),
                season,
                name: "2nd")
            {
                FrameCount = 18,
                MinPlayerClass = 1,
                RoundsPerSeasonCount = 2,
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

            season.Divisions.Add(division2);

            Division division3 = new Division(
                id: Guid.Parse("c6e35a27-f9ae-4506-830f-75b7d01a9fa2"),
                season,
                name: "3rd")
            {
                FrameCount = 12,
                MinPlayerClass = 2,
                RoundsPerSeasonCount = 2,
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

            season.Divisions.Add(division3);

            Division division4 = new Division(
                id: Guid.Parse("4ce1630d-fd5c-42cb-b6c4-3100a1d32d60"),
                season,
                name: "4th")
            {
                FrameCount = 12,
                MinPlayerClass = 2,
                RoundsPerSeasonCount = 2,
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

            season.Divisions.Add(division4);

            Division division5 = new Division(
                id: Guid.Parse("953a5174-55b4-4e81-973a-a6d25aa1195b"),
                season,
                name: "5th")
            {
                FrameCount = 9,
                MinPlayerClass = 3,
                RoundsPerSeasonCount = 2,
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

            season.Divisions.Add(division5);

            Division divisionSaturday = new Division(
                id: Guid.Parse("65ec4fde-a054-4816-8244-5e0d149751a4"),
                season,
                name: "Saturday")
            {
                FrameCount = 12,
                MinPlayerClass = 3,
                RoundsPerSeasonCount = 3,
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

            season.Divisions.Add(divisionSaturday);

            // Act
            await _seasonManager.ScheduleAsync(season);

            // Assert
        });
    }
}