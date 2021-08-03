using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Snooker.ClubPlayers
{
    public class ClubPlayerManager : DomainService
    {
        private readonly IClubPlayerRepository _clubPlayerRepository;

        public ClubPlayerManager(IClubPlayerRepository clubPlayerRepository)
        {
            _clubPlayerRepository = clubPlayerRepository;
        }

        public async Task<ClubPlayer> CreateClubPlayerAsync(Guid clubId, Guid playerId)
        {
            ClubPlayer clubPlayer = await _clubPlayerRepository.FindAsync(x =>
                x.ClubId == clubId && x.PlayerId == playerId);

            if (clubPlayer != null)
            {
                throw new BusinessException(SnookerDomainErrorCodes.ClubPlayers.PlayerAlreadyLinkedToClub);
            }

            return new ClubPlayer(GuidGenerator.Create(), clubId, playerId);
        }
    }
}