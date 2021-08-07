using Shouldly;
using Snooker.Players;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Snooker.MyPlayer
{
    public class MyPlayerAppServiceTests : SnookerApplicationTestBase
    {
        private readonly IMyPlayerAppService _myPlayerAppService;

        public MyPlayerAppServiceTests()
        {
            _myPlayerAppService = GetRequiredService<IMyPlayerAppService>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            PlayerDto result = await _myPlayerAppService.GetAsync();

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));
            result.FirstName.ShouldBe("John");
            result.LastName.ShouldBe("Doe");
            result.UserId.ShouldBe(Guid.Parse("4a05a121-7e89-4998-bb46-9d88cc49973f"));
        }
    }
}