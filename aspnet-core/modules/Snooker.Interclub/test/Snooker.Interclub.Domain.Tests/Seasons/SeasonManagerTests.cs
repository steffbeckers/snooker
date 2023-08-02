using Snooker.Interclub.Clubs;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Teams;
using System;
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
        // Arrange
        Season season = new Season(
            id: Guid.Parse("1877aa91-3906-48f0-a4eb-eb7dc9358835"),
            startDate: new DateTime(_clock.Now.Year, 1, 1),
            endDate: new DateTime(_clock.Now.AddYears(1).Year, 1, 1));

        Club club1 = await _clubManager.CreateAsync(
            id: Guid.Parse("d1610828-8df5-4ced-8bf3-d876f6207691"),
            name: "Biljart Lounge");
        club1.NumberOfTables = 4;

        Club club2 = await _clubManager.CreateAsync(
            id: Guid.Parse("1252eaad-0efa-433f-a598-4909158de769"),
            name: "Buckingham");
        club2.NumberOfTables = 8;

        Club club3 = await _clubManager.CreateAsync(
            id: Guid.Parse("c95db223-c2c3-49e6-b0cd-d823327d1814"),
            name: "De Kreeft");
        club3.NumberOfTables = 6;

        Club club4 = await _clubManager.CreateAsync(
            id: Guid.Parse("a0d1964b-92c1-42dd-a5a9-a45a21f1279c"),
            name: "De Maxx");
        club4.NumberOfTables = 10;

        Club club5 = await _clubManager.CreateAsync(
            id: Guid.Parse("9a03e899-1054-4e48-86e3-ff7250d48eb5"),
            name: "Happy Snooker");
        club5.NumberOfTables = 5;

        Club club6 = await _clubManager.CreateAsync(
            id: Guid.Parse("d72092b9-1476-4954-914a-2bfb1c8c73e6"),
            name: "NRG");
        club6.NumberOfTables = 3;

        Club club7 = await _clubManager.CreateAsync(
            id: Guid.Parse("e6071606-563e-4e73-beb6-962dfe3df769"),
            name: "Re-Spot");
        club7.NumberOfTables = 6;

        Club club8 = await _clubManager.CreateAsync(
            id: Guid.Parse("19d70aac-ba91-4a53-84b4-69cf4e694f0a"),
            name: "Riley Inn");
        club8.NumberOfTables = 8;

        Club club9 = await _clubManager.CreateAsync(
            id: Guid.Parse("95adc0ff-e6ab-4d52-aa64-82f4849228fc"),
            name: "Snooker Sports");
        club9.NumberOfTables = 3;

        Club club10 = await _clubManager.CreateAsync(
            id: Guid.Parse("e8fb5f9d-b5a5-4456-83bf-43282490e87b"),
            name: "Zuma");
        club10.NumberOfTables = 6;

        Division division1 = new Division(
            id: Guid.Parse("3c7923fd-8254-4680-8aa2-8fc3aa46cb16"),
            season,
            name: "Ere")
        {
            FrameCount = 18,
            MinPlayerClass = 1,
            RoundsPerSeasonCount = 2,
            SortOrder = 1
        };

        Team division1Club1Team1 = new Team(
            id: Guid.Parse("d7fe88a4-7384-4000-a78b-5f7ae68aa077"),
            division1,
            club1,
            name: "A");
        division1.Teams.Add(division1Club1Team1);
        club1.Teams.Add(division1Club1Team1);

        season.Divisions.Add(division1);

        Division division2 = new Division(
            id: Guid.Parse("47a1a8b5-7845-4767-be7c-ed5b13af2ca7"),
            season,
            name: "1st")
        {
            FrameCount = 18,
            MinPlayerClass = 1,
            RoundsPerSeasonCount = 2,
            SortOrder = 2
        };

        Team division2Club1Team2 = new Team(
            id: Guid.Parse("22893950-166a-4498-ad2c-6a2b5552652c"),
            division2,
            club1,
            name: "B");
        division2.Teams.Add(division2Club1Team2);
        club1.Teams.Add(division2Club1Team2);

        season.Divisions.Add(division2);

        Division division3 = new Division(
            id: Guid.Parse("0842c4cf-4579-4f01-b2e7-0c90141ffee8"),
            season,
            name: "2nd")
        {
            FrameCount = 18,
            MinPlayerClass = 1,
            RoundsPerSeasonCount = 2,
            SortOrder = 3
        };

        season.Divisions.Add(division3);

        Division division4 = new Division(
            id: Guid.Parse("c6e35a27-f9ae-4506-830f-75b7d01a9fa2"),
            season,
            name: "3rd")
        {
            FrameCount = 12,
            MinPlayerClass = 2,
            RoundsPerSeasonCount = 2,
            SortOrder = 4
        };

        season.Divisions.Add(division4);

        Division division5 = new Division(
            id: Guid.Parse("4ce1630d-fd5c-42cb-b6c4-3100a1d32d60"),
            season,
            name: "4th")
        {
            FrameCount = 12,
            MinPlayerClass = 2,
            RoundsPerSeasonCount = 2,
            SortOrder = 5
        };

        season.Divisions.Add(division5);

        Division division6 = new Division(
            id: Guid.Parse("953a5174-55b4-4e81-973a-a6d25aa1195b"),
            season,
            name: "5th")
        {
            FrameCount = 9,
            MinPlayerClass = 3,
            RoundsPerSeasonCount = 2,
            SortOrder = 6
        };

        season.Divisions.Add(division6);

        Division division7 = new Division(
            id: Guid.Parse("65ec4fde-a054-4816-8244-5e0d149751a4"),
            season,
            name: "Saturday")
        {
            FrameCount = 12,
            MinPlayerClass = 3,
            RoundsPerSeasonCount = 2,
            SortOrder = 7
        };

        season.Divisions.Add(division7);

        // Act
        await _seasonManager.ScheduleAsync(season);

        // Assert
    }
}