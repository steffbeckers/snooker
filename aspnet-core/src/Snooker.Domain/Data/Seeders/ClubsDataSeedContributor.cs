using Snooker.Clubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Snooker.Data.Seeders
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
            if (await _clubRepository.GetCountAsync() > 0) return;

            List<Club> clubs = new List<Club>()
            {
                new Club(Guid.Parse("2c9332b8-8500-4df8-9ba2-ceb7100bbcf0"), "Biljart Lounge"),
                new Club(Guid.Parse("5a43123c-2e24-4777-baea-253765895593"), "Buckingham"),
                new Club(Guid.Parse("193ab0f0-3098-497c-aca9-82fe464d41eb"), "De Kreeft"),
                new Club(Guid.Parse("ba5c4be0-fa80-4751-95ae-df1388bed9f6"), "De Maxx"),
                new Club(Guid.Parse("6742d84c-7348-42ed-8d12-ce560b42879e"), "Erasmus"),
                new Club(Guid.Parse("45a0daf3-f2aa-4cff-a202-1a57c195dada"), "Happy Snooker"),
                new Club(Guid.Parse("161da449-c4e5-4c54-b57f-de7fa7e635ab"), "NRG"),
                new Club(Guid.Parse("09db72af-ffbb-4068-a3cb-d211ca757a5d"), "Play 147"),
                new Club(Guid.Parse("deee185d-64ff-4041-a7b0-7d574da4f4ee"), "Play Ball"),
                new Club(Guid.Parse("9b0b972c-8697-47e5-99b1-71f2c5e1ba55"), "Re-Spot"),
                new Club(Guid.Parse("f3c33b5d-e3fd-48ce-9d85-3f91480b0b4a"), "Riley Inn"),
                new Club(Guid.Parse("0e17af1d-3bcc-45ea-bc24-f5894f2701d3"), "Snooker Sports"),
                new Club(Guid.Parse("e57f7faf-d74d-46d6-ad6e-0fe91c8093fc"), "Zuma")
            };

            await _clubRepository.InsertManyAsync(clubs);
        }
    }
}