using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Snooker.Clubs
{
    public class ClubsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IClubRepository _clubRepository;

        public ClubsDataSeedContributor(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _clubRepository.InsertAsync(new Club
            (
                id: Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                name: "c724f8dbebb842ed80ad9868fa0b01ce5b3c6b05709a4ed6a757de142a6822b89788c5f5bc6641db8d5a868f35708ce3cda5"
            ));

            await _clubRepository.InsertAsync(new Club
            (
                id: Guid.Parse("51b646da-9b05-454a-8fed-39371f1c6710"),
                name: "6eaaca6a0b3a4d5fa93a57d7ffe3a1d20b2a6c5d77304defbffdc7448854af541a489cc1ecee4fafb35e2ccc36dd1d4c12a9"
            ));
        }
    }
}