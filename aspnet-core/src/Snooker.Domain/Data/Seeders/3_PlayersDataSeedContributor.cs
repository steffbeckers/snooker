using Snooker.Players;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Snooker.Data.Seeders
{
    public class PlayersDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersDataSeedContributor(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _playerRepository.GetCountAsync() > 0) return;

            List<Player> players = new List<Player>()
            {
                new Player(Guid.Parse("2c9332b8-8500-4df8-9ba2-ceb7100bbcf0"), "Steff", "Beckers"),
                new Player(Guid.Parse("dcac11b3-c666-44c9-979a-3160c62d44cc"), "Marco", "Vitali"),
                new Player(Guid.Parse("0362fe6b-c173-4602-873f-a08b9b49fe80"), "Ronny", "Bekkers"),
                new Player(Guid.Parse("fca989ed-d314-4121-9fbe-e52dc81f5a9a"), "Ronny", "Foets")
            };

            await _playerRepository.InsertManyAsync(players);
        }
    }
}