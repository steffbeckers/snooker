using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Snooker.Players
{
    public class PlayerManager : DomainService, ITransientDependency
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly IPlayerRepository _playerRepository;

        public PlayerManager(
            IPlayerRepository playerRepository,
            IdentityUserManager identityUserManager)
        {
            _playerRepository = playerRepository;
            _identityUserManager = identityUserManager;
        }

        public async Task LinkUserToPlayer(Player player, IdentityUser user)
        {
            Player existingPlayer = await _playerRepository.FindAsync(x => x.Id != player.Id && x.UserId == user.Id);
            if (existingPlayer != null)
            {
                // TODO: Localization
                throw new UserFriendlyException($"User is already linked to player with Id '{existingPlayer.Id}'.");
            }

            player.UserId = user.Id;
        }
    }
}