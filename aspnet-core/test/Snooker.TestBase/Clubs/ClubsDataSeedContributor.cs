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
            await _clubRepository.InsertAsync(new Club(
                id: Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                name: "Snooker Club 1"));

            await _clubRepository.InsertAsync(new Club(
                id: Guid.Parse("51b646da-9b05-454a-8fed-39371f1c6710"),
                name: "Snooker Club 2"));
        }
    }
}