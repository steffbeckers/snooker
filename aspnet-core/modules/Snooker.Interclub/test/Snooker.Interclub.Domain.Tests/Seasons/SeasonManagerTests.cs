using System;
using System.Threading.Tasks;
using Xunit;

namespace Snooker.Interclub.Seasons;

public class SeasonManagerTests : InterclubDomainTestBase
{
    private readonly SeasonManager _seasonManager;
    private readonly ISeasonRepository _seasonRepository;

    public SeasonManagerTests()
    {
        _seasonManager = GetRequiredService<SeasonManager>();
        _seasonRepository = GetRequiredService<ISeasonRepository>();
    }

    [Fact]
    public async Task Should_Schedule_Season()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            // Arrange
            Guid seasonId = Guid.Parse("1877aa91-3906-48f0-a4eb-eb7dc9358835"); // Season 22-23

            // Act
            Season season = await _seasonManager.ScheduleAsync(seasonId);

            // Assert
        });
    }
}