using Shouldly;
using Snooker.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerRepositoryTests : SnookerEntityFrameworkCoreTestBase
    {
        private readonly IClubPlayerRepository _clubPlayerRepository;

        public ClubPlayerRepositoryTests()
        {
            _clubPlayerRepository = GetRequiredService<IClubPlayerRepository>();
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                long result = await _clubPlayerRepository.GetCountAsync(
                    clubId: Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                    playerId: Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d")
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
                List<ClubPlayer> result = await _clubPlayerRepository.GetListAsync(
                    clubId: Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                    playerId: Guid.Parse("7b0b86ff-df6b-4b5f-8f6c-a226d00949cc")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("7278ac73-6d82-459b-9289-aa760dcb2722"));
            });
        }
    }
}