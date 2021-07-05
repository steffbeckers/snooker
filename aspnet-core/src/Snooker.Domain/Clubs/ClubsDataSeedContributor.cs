using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Guids;

namespace Snooker.Clubs
{
    public class ClubsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IClubRepository _clubRepository;
        private readonly IGuidGenerator _guidGenerator;

        public ClubsDataSeedContributor(
            IClubRepository clubRepository,
            IGuidGenerator guidGenerator)
        {
            _clubRepository = clubRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _clubRepository.GetCountAsync() > 0) return;

            List<Club> clubs = new List<Club>()
            {
                new Club(_guidGenerator.Create(), "Biljart Lounge"),
                new Club(_guidGenerator.Create(), "Buckingham"),
                new Club(_guidGenerator.Create(), "De Kreeft"),
                new Club(_guidGenerator.Create(), "De Maxx"),
                new Club(_guidGenerator.Create(), "Erasmus"),
                new Club(_guidGenerator.Create(), "Happy Snooker"),
                new Club(_guidGenerator.Create(), "NRG"),
                new Club(_guidGenerator.Create(), "Play 147"),
                new Club(_guidGenerator.Create(), "Play Ball"),
                new Club(_guidGenerator.Create(), "Re-Spot"),
                new Club(_guidGenerator.Create(), "Riley Inn"),
                new Club(_guidGenerator.Create(), "Snooker Sports"),
                new Club(_guidGenerator.Create(), "Zuma")
            };

            await _clubRepository.InsertManyAsync(clubs);
        }
    }
}