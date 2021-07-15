using Shouldly;
using Snooker.ClubPlayers;
using Snooker.Clubs;
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
        public async Task GetClubAsync()
        {
            // Act
            ClubDto result = await _playersAppService.GetClubAsync(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));

            // Assert
            result.Id.ShouldBe(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));
            result.Name.ShouldBe("c724f8dbebb842ed80ad9868fa0b01ce5b3c6b05709a4ed6a757de142a6822b89788c5f5bc6641db8d5a868f35708ce3cda5");
        }

        [Fact]
        public async Task GetClubsListAsync()
        {
            // Act
            PagedResultDto<ClubPlayerListDto> result = await _playersAppService.GetClubsListAsync(
                Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"),
                new GetClubPlayersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("2bbaae46-29fe-4b1c-99f8-63a9296204cf")).ShouldBe(true);

            ClubPlayerListDto clubPlayerListDto = result.Items.First(x => x.Id == Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2"));
            clubPlayerListDto.Id.ShouldBe(Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2"));
            clubPlayerListDto.Club.Id.ShouldBe(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));
            clubPlayerListDto.Club.Name.ShouldBe("c724f8dbebb842ed80ad9868fa0b01ce5b3c6b05709a4ed6a757de142a6822b89788c5f5bc6641db8d5a868f35708ce3cda5");
            clubPlayerListDto.IsPrimaryClubOfPlayer.ShouldBeTrue();
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
        public async Task GetProfileAsync()
        {
            // Act
            PlayerProfileDto result = await _playersAppService.GetProfileAsync(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));
            result.FirstName.ShouldBe("c834078813df481ca791798436068aa463dd9a2a58f84c4ea4");
            result.LastName.ShouldBe("da78c9ae5a414229835179bc737a7b11e5739bef966448cdb0");
            result.Club.Id.ShouldBe(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));
            result.Club.Name.ShouldBe("c724f8dbebb842ed80ad9868fa0b01ce5b3c6b05709a4ed6a757de142a6822b89788c5f5bc6641db8d5a868f35708ce3cda5");
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