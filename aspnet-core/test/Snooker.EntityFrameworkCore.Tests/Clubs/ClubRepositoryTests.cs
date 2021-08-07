using Shouldly;
using Snooker.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Snooker.Clubs
{
    public class ClubRepositoryTests : SnookerEntityFrameworkCoreTestBase
    {
        private readonly IClubRepository _clubRepository;

        public ClubRepositoryTests()
        {
            _clubRepository = GetRequiredService<IClubRepository>();
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                long result = await _clubRepository.GetCountAsync(
                    name: "Snooker Club 2"
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
                List<Club> result = await _clubRepository.GetListAsync(
                    name: "Snooker Club 1"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));
            });
        }
    }
}