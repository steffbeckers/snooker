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
using Volo.Abp.ObjectMapping;

namespace Snooker.Players
{
    [RemoteService(IsEnabled = false)]
    [Authorize(SnookerPermissions.Players.Default)]
    public class PlayersAppService : ApplicationService, IPlayersAppService
    {
        private readonly IClubPlayerRepository _clubPlayerRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayersAppService(
            IClubPlayerRepository clubPlayerRepository,
            IClubRepository clubRepository,
            IPlayerRepository playerRepository)
        {
            _clubPlayerRepository = clubPlayerRepository;
            _clubRepository = clubRepository;
            _playerRepository = playerRepository;
        }

        [Authorize(SnookerPermissions.Players.Create)]
        public virtual async Task<PlayerDto> CreateAsync(PlayerCreateDto input)
        {
            Player player = new Player(
                GuidGenerator.Create(),
                input.FirstName,
                input.LastName);

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