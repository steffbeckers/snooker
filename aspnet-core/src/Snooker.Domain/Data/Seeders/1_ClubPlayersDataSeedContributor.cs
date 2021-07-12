using Snooker.ClubPlayers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Snooker.Data.Seeders
{
    public class ClubPlayersDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IClubPlayerRepository _clubPlayerRepository;

        public ClubPlayersDataSeedContributor(IClubPlayerRepository clubPlayerRepository)
        {
            _clubPlayerRepository = clubPlayerRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _clubPlayerRepository.GetCountAsync() > 0) return;

            Guid nrgClubId = Guid.Parse("161da449-c4e5-4c54-b57f-de7fa7e635ab");

            List<ClubPlayer> clubPlayers = new List<ClubPlayer>()
            {
                // Steff Beckers
                new ClubPlayer(
                    Guid.Parse("96b862ec-5948-43b9-af82-eb1746e1f81a"),
                    nrgClubId,
                    Guid.Parse("2c9332b8-8500-4df8-9ba2-ceb7100bbcf0")),
                // Marco Vitali
                new ClubPlayer(
                    Guid.Parse("af8ad872-f724-4232-8522-dffdc3a15fdb"),
                    nrgClubId,
                    Guid.Parse("dcac11b3-c666-44c9-979a-3160c62d44cc")),
                // Ronny Bekkers
                new ClubPlayer(
                    Guid.Parse("ebf15cc9-50b1-4356-8d4c-a3eb0ae1643c"),
                    nrgClubId,
                    Guid.Parse("0362fe6b-c173-4602-873f-a08b9b49fe80")),
                // Ronny Foets
                new ClubPlayer(
                    Guid.Parse("846f4109-4a56-460b-87a8-df49b0bcc86e"),
                    nrgClubId,
                    Guid.Parse("fca989ed-d314-4121-9fbe-e52dc81f5a9a"))
            };

            await _clubPlayerRepository.InsertManyAsync(clubPlayers);
        }
    }
}