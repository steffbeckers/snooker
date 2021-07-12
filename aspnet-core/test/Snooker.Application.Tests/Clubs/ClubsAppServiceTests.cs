using Shouldly;
using Snooker.ClubPlayers;
using Snooker.Players;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace Snooker.Clubs
{
    public class ClubsAppServiceTests : SnookerApplicationTestBase
    {
        private readonly IClubRepository _clubRepository;
        private readonly IClubsAppService _clubsAppService;

        public ClubsAppServiceTests()
        {
            _clubRepository = GetRequiredService<IClubRepository>();
            _clubsAppService = GetRequiredService<IClubsAppService>();
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            ClubCreateDto input = new ClubCreateDto
            {
                Name = "bba11c3003ac4085a238b470f78c262d18e0a974712a4fd4a3bd405ba7521daadb7473f92d85418fb1acb8bc81880207ebf2"
            };

            // Act
            ClubDto serviceResult = await _clubsAppService.CreateAsync(input);

            // Assert
            Club result = await _clubRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("bba11c3003ac4085a238b470f78c262d18e0a974712a4fd4a3bd405ba7521daadb7473f92d85418fb1acb8bc81880207ebf2");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _clubsAppService.DeleteAsync(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));

            // Assert
            Club result = await _clubRepository.FindAsync(c => c.Id == Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));

            result.ShouldBeNull();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            ClubDto result = await _clubsAppService.GetAsync(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            PagedResultDto<ClubListDto> result = await _clubsAppService.GetListAsync(
                new GetClubsInput() { MaxResultCount = 20 });

            // Assert
            result.TotalCount.ShouldBe(15);
            result.Items.Count.ShouldBe(15);
            result.Items.Any(x => x.Id == Guid.Parse("d772238a-9871-47d7-84d5-c45083799954")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("51b646da-9b05-454a-8fed-39371f1c6710")).ShouldBe(true);
        }

        [Fact]
        public async Task GetPlayersListAsync()
        {
            // Act
            PagedResultDto<ClubPlayerListDto> result = await _clubsAppService.GetPlayersListAsync(
                Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                new GetClubPlayersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("7278ac73-6d82-459b-9289-aa760dcb2722")).ShouldBe(true);

            ClubPlayerListDto clubPlayerListDto = result.Items.FirstOrDefault(x => x.Id == Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2"));
            clubPlayerListDto.Id.ShouldBe(Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2"));
            clubPlayerListDto.Player.Id.ShouldBe(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));
            clubPlayerListDto.Player.FirstName.ShouldBe("c834078813df481ca791798436068aa463dd9a2a58f84c4ea4");
            clubPlayerListDto.Player.LastName.ShouldBe("da78c9ae5a414229835179bc737a7b11e5739bef966448cdb0");
            clubPlayerListDto.IsPrimaryClubOfPlayer.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            ClubUpdateDto input = new ClubUpdateDto()
            {
                Name = "143871a4e4294ced94757d8ba4e861f2ab712c1241e24d058540666876d7289b02b17a8cc7d3431ab37381fa6923f624de67"
            };

            // Act
            ClubDto serviceResult = await _clubsAppService.UpdateAsync(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"), input);

            // Assert
            Club result = await _clubRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("143871a4e4294ced94757d8ba4e861f2ab712c1241e24d058540666876d7289b02b17a8cc7d3431ab37381fa6923f624de67");
        }
    }
}