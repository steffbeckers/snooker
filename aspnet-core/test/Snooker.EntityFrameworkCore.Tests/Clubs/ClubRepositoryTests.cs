using Shouldly;
using Snooker.EntityFrameworkCore;
using System;
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
                var result = await _clubRepository.GetCountAsync(
                    name: "6eaaca6a0b3a4d5fa93a57d7ffe3a1d20b2a6c5d77304defbffdc7448854af541a489cc1ecee4fafb35e2ccc36dd1d4c12a9"
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
                var result = await _clubRepository.GetListAsync(
                    name: "c724f8dbebb842ed80ad9868fa0b01ce5b3c6b05709a4ed6a757de142a6822b89788c5f5bc6641db8d5a868f35708ce3cda5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"));
            });
        }
    }
}