using Shouldly;
using Snooker.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Snooker.Players
{
    public class PlayerRepositoryTests : SnookerEntityFrameworkCoreTestBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerRepositoryTests()
        {
            _playerRepository = GetRequiredService<IPlayerRepository>();
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                long result = await _playerRepository.GetCountAsync(
                    firstName: "8a2507a2413b443f8ebfd8e959e6d399a6c3f53aa5a444cd9f",
                    lastName: "0c3dd4065927406da16668a73df94b7ec52390b6679c47b6a5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                List<Player> result = await _playerRepository.GetListAsync(
                    firstName: "c834078813df481ca791798436068aa463dd9a2a58f84c4ea4",
                    lastName: "da78c9ae5a414229835179bc737a7b11e5739bef966448cdb0"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));
            });
        }
    }
}