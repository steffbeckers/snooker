using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Snooker.ClubPlayers
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
            await _clubPlayerRepository.InsertAsync(new ClubPlayer
            (
                id: Guid.Parse("d6f4c147-22a2-475b-80bb-bacf18d08ce2"),
                clubId: Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                playerId: Guid.Parse("85ea0ccf-0fad-4c6f-b660-23e6004a777d")
            ));

            await _clubPlayerRepository.InsertAsync(new ClubPlayer
            (
                id: Guid.Parse("7278ac73-6d82-459b-9289-aa760dcb2722"),
                clubId: Guid.Parse("d772238a-9871-47d7-84d5-c45083799954"),
                playerId: Guid.Parse("7b0b86ff-df6b-4b5f-8f6c-a226d00949cc")
            ));
        }
    }
}