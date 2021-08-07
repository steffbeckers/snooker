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
                    firstName: "Jane",
                    lastName: "Doe"
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
                    firstName: "John",
                    lastName: "Doe"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"));
            });
        }
    }
}