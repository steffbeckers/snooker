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
                firstName: "John",
                lastName: "Doe")
            {
                UserId = Guid.Parse("4a05a121-7e89-4998-bb46-9d88cc49973f")
            });

            await _playerRepository.InsertAsync(new Player(
                id: Guid.Parse("7b0b86ff-df6b-4b5f-8f6c-a226d00949cc"),
                firstName: "Jane",
                lastName: "Doe"));
        }
    }
}