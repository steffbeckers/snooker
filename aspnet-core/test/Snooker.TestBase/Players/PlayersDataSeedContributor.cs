using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Snooker.Players
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
            await _playerRepository.InsertAsync(new Player(
                id: Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d"),
                firstName: "c834078813df481ca791798436068aa463dd9a2a58f84c4ea4",
                lastName: "da78c9ae5a414229835179bc737a7b11e5739bef966448cdb0"));

            await _playerRepository.InsertAsync(new Player(
                id: Guid.Parse("7b0b86ff-df6b-4b5f-8f6c-a226d00949cc"),
                firstName: "8a2507a2413b443f8ebfd8e959e6d399a6c3f53aa5a444cd9f",
                lastName: "0c3dd4065927406da16668a73df94b7ec52390b6679c47b6a5"));
        }
    }
}