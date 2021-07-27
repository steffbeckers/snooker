using Microsoft.AspNetCore.Authorization;
using Snooker.ClubPlayers;
using Snooker.Clubs;
using Snooker.Permissions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

namespace Snooker.Players
{
    [RemoteService(IsEnabled = false)]
    [Authorize(SnookerPermissions.Players.Default)]
    public class PlayersAppService : SnookerAppService, IPlayersAppService
    {
        private readonly IClubPlayerRepository _clubPlayerRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly PlayerManager _playerManager;
        private readonly IPlayerRepository _playerRepository;

        public PlayersAppService(
            IClubPlayerRepository clubPlayerRepository,
            IClubRepository clubRepository,
            IdentityUserManager identityUserManager,
            IIdentityUserRepository identityUserRepository,
            PlayerManager playerManager,
            IPlayerRepository playerRepository)
        {
            _clubPlayerRepository = clubPlayerRepository;
            _clubRepository = clubRepository;
            _identityUserManager = identityUserManager;
            _identityUserRepository = identityUserRepository;
            _playerManager = playerManager;
            _playerRepository = playerRepository;
        }

        [Authorize(SnookerPermissions.Players.Create)]
        public virtual async Task<PlayerDto> CreateAsync(PlayerCreateDto input)
        {
            Player player = new Player(
                GuidGenerator.Create(),
                input.FirstName,
                input.LastName);

            if (input.UserId.HasValue)
            {
                IdentityUser user = await _identityUserRepository.GetAsync(input.UserId.Value);
                await _playerManager.LinkUserToPlayer(player, user);
            }
            else if (!string.IsNullOrEmpty(input.Email))
            {
                IdentityUser user = await _identityUserRepository.FindByNormalizedEmailAsync(input.Email);
                if (user == null)
                {
                    user = new IdentityUser(
                        GuidGenerator.Create(),
                        input.Email.ToLower(),
                        input.Email.ToLower());
                    await _identityUserManager.CreateAsync(user, input.Password);
                }

                await _playerManager.LinkUserToPlayer(player, user);
            }

            player = await _playerRepository.InsertAsync(player, autoSave: true);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }

        [Authorize(SnookerPermissions.Players.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _playerRepository.DeleteAsync(id);
        }

        public virtual async Task<PlayerDto> GetAsync(Guid id)
        {
            Player player = await _playerRepository.GetAsync(id);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }

        public virtual async Task<ClubDto> GetClubAsync(Guid id)
        {
            ClubPlayer clubPlayer = await _clubPlayerRepository.FindAsync(x => x.PlayerId == id && x.IsPrimaryClubOfPlayer);
            if (clubPlayer == null)
            {
                return null;
            }

            IQueryable<Club> clubQueryable = await _clubRepository.GetQueryableAsync();

            IQueryable<ClubDto> clubDtoQueryable = ObjectMapper
                .GetMapper()
                .ProjectTo<ClubDto>(clubQueryable.Where(x => x.Id == clubPlayer.ClubId));

            return await AsyncExecuter.FirstOrDefaultAsync(clubDtoQueryable);
        }

        [Authorize(SnookerPermissions.Clubs.Default)]
        public virtual async Task<PagedResultDto<ClubPlayerListDto>> GetClubsListAsync(Guid id, GetClubPlayersInput input)
        {
            long totalCount = await _clubPlayerRepository.GetCountAsync(
                input.FilterText,
                null,
                id,
                input.IsPrimaryClubOfPlayer);

            IQueryable<ClubPlayer> clubPlayerQueryable = await _clubPlayerRepository.GetFilteredQueryableAsync(
                input.FilterText,
                null,
                id,
                input.IsPrimaryClubOfPlayer,
                input.Sorting,
                input.MaxResultCount);

            IQueryable<Club> clubQueryable = await _clubRepository.GetFilteredQueryableAsync(input.FilterText);

            IQueryable<ClubPlayerWithNavigationProperties> clubPlayerWithNavigationPropertiesQueryable = clubPlayerQueryable.Join(
                clubQueryable,
                x => x.ClubId,
                x => x.Id,
                (clubPlayer, club) => new ClubPlayerWithNavigationProperties()
                {
                    Id = clubPlayer.Id,
                    ClubId = clubPlayer.ClubId,
                    Club = club,
                    PlayerId = clubPlayer.PlayerId,
                    IsPrimaryClubOfPlayer = clubPlayer.IsPrimaryClubOfPlayer
                });

            IQueryable<ClubPlayerListDto> clubPlayerListDtoQueryable = ObjectMapper
                .GetMapper()
                .ProjectTo<ClubPlayerListDto>(clubPlayerWithNavigationPropertiesQueryable);

            return new PagedResultDto<ClubPlayerListDto>()
            {
                TotalCount = totalCount,
                Items = await AsyncExecuter.ToListAsync(clubPlayerListDtoQueryable)
            };
        }

        public virtual async Task<PagedResultDto<PlayerListDto>> GetListAsync(GetPlayersInput input)
        {
            long totalCount = await _playerRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName, input.UserId);

            IQueryable<Player> playerQueryable = await _playerRepository.GetFilteredQueryableAsync(
                input.FilterText,
                input.FirstName,
                input.LastName,
                input.UserId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            IQueryable<PlayerListDto> playerListDtoQueryable = ObjectMapper.GetMapper().ProjectTo<PlayerListDto>(playerQueryable);

            return new PagedResultDto<PlayerListDto>
            {
                TotalCount = totalCount,
                Items = await AsyncExecuter.ToListAsync(playerListDtoQueryable)
            };
        }

        public virtual async Task<PlayerProfileDto> GetProfileAsync(Guid id)
        {
            IQueryable<Player> playerQueryable = await _playerRepository.GetQueryableAsync();
            IQueryable<ClubPlayer> clubPlayerQueryable = await _clubPlayerRepository.GetQueryableAsync();
            IQueryable<Club> clubQueryable = await _clubRepository.GetQueryableAsync();

            IQueryable<ClubPlayerWithNavigationProperties> clubPlayerWithNavigationPropertiesQueryable = playerQueryable.Where(x => x.Id == id)
                .SelectMany(x => clubPlayerQueryable.Where(y => y.PlayerId == id && y.IsPrimaryClubOfPlayer).DefaultIfEmpty(),
                (player, clubPlayer) => new { Player = player, ClubPlayer = clubPlayer })
                .SelectMany(x => clubQueryable.Where(y => y.Id == x.ClubPlayer.ClubId).DefaultIfEmpty(),
                (x, club) => new ClubPlayerWithNavigationProperties()
                {
                    Player = x.Player,
                    Club = club,
                });

            IQueryable<PlayerProfileDto> playerProfileDtoQueryable = ObjectMapper.GetMapper().ProjectTo<PlayerProfileDto>(clubPlayerWithNavigationPropertiesQueryable);

            return await AsyncExecuter.FirstOrDefaultAsync(playerProfileDtoQueryable);
        }

        [Authorize(SnookerPermissions.Players.Edit)]
        public virtual async Task<PlayerDto> UpdateAsync(Guid id, PlayerUpdateDto input)
        {
            Player player = await _playerRepository.GetAsync(id);

            player.FirstName = input.FirstName;
            player.LastName = input.LastName;
            // TODO: Assign user through player manager?
            player.UserId = input.UserId;

            player = await _playerRepository.UpdateAsync(player, autoSave: true);

            return ObjectMapper.Map<Player, PlayerDto>(player);
        }
    }
}