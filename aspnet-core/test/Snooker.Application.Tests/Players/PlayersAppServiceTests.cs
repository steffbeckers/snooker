using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace Snooker.Players
{
    public class PlayersAppServiceTests : SnookerApplicationTestBase
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayersAppService _playersAppService;

        public PlayersAppServiceTests()
        {
            _playerRepository = GetRequiredService<IPlayerRepository>();
            _playersAppService = GetRequiredService<IPlayersAppService>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            PlayerCreateDto input = new PlayerCreateDto
            {
                FirstName = "e3932db638d248c384f42299175559e087ad8a304e7041aba2",
                LastName = "5afda06d26e448b5b03a290d0efad5fe9b5adb78bda44732bd"
            };

            // Act
            PlayerDto serviceResult = await _playersAppService.CreateAsync(input);

            // Assert
            Player result = await _playerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.FirstName.ShouldBe("e3932db638d248c384f42299175559e087ad8a304e7041aba2");
            result.LastName.ShouldBe("5afda06d26e448b5b03a290d0efad5fe9b5adb78bda44732bd");
            result.UserId.ShouldBeNull();
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _playersAppService.DeleteAsync(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));

            // Assert
            Player result = await _playerRepository.FindAsync(c => c.Id == Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));

            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            PlayerDto result = await _playersAppService.GetAsync(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            PagedResultDto<PlayerListDto> result = await _playersAppService.GetListAsync(new GetPlayersInput());

            // Assert
            result.TotalCount.ShouldBe(6);
            result.Items.Count.ShouldBe(6);
            result.Items.Any(x => x.Id == Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("7b0b86ff-df6b-4b5f-8f6c-a226d00949cc")).ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            PlayerUpdateDto input = new PlayerUpdateDto()
            {
                FirstName = "3a391adb63814390bfffd312569af8c983856925cd0546fb86",
                LastName = "727a939d47fd49fcb7dc03c8231ad571ae210cb7fc634ef6b1"
            };

            // Act
            PlayerDto serviceResult = await _playersAppService.UpdateAsync(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"), input);

            // Assert
            Player result = await _playerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.FirstName.ShouldBe("3a391adb63814390bfffd312569af8c983856925cd0546fb86");
            result.LastName.ShouldBe("727a939d47fd49fcb7dc03c8231ad571ae210cb7fc634ef6b1");
            result.UserId.ShouldBeNull();
        }
    }
}