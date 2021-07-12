using Microsoft.AspNetCore.Authorization;
using Snooker.ClubPlayers;
using Snooker.Permissions;
using Snooker.Players;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Snooker.Clubs
{
    [RemoteService(IsEnabled = false)]
    [Authorize(SnookerPermissions.Clubs.Default)]
    public class ClubsAppService : SnookerAppService, IClubsAppService
    {
        private readonly IClubPlayerRepository _clubPlayerRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IPlayerRepository _playerRepository;

        public ClubsAppService(
            IClubPlayerRepository clubPlayerRepository,
            IClubRepository clubRepository,
            IPlayerRepository playerRepository)
        {
            _clubPlayerRepository = clubPlayerRepository;
            _clubRepository = clubRepository;
            _playerRepository = playerRepository;
        }

        [Authorize(SnookerPermissions.Clubs.Create)]
        public virtual async Task<ClubDto> CreateAsync(ClubCreateDto input)
        {
            Club club = new Club(GuidGenerator.Create(), input.Name);

            club = await _clubRepository.InsertAsync(club, autoSave: true);

            return ObjectMapper.Map<Club, ClubDto>(club);
        }

        [Authorize(SnookerPermissions.Clubs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _clubRepository.DeleteAsync(id);
        }

        public virtual async Task<ClubDto> GetAsync(Guid id)
        {
            Club club = await _clubRepository.GetAsync(id);

            return ObjectMapper.Map<Club, ClubDto>(club);
        }

        public virtual async Task<PagedResultDto<ClubListDto>> GetListAsync(GetClubsInput input)
        {
            long totalCount = await _clubRepository.GetCountAsync(input.FilterText, input.Name);

            IQueryable<Club> clubQueryable = await _clubRepository.GetFilteredQueryableAsync(
                input.FilterText,
                input.Name,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            IQueryable<ClubListDto> clubListDtoQueryable = ObjectMapper.GetMapper().ProjectTo<ClubListDto>(clubQueryable);

            return new PagedResultDto<ClubListDto>()
            {
                TotalCount = totalCount,
                Items = await AsyncExecuter.ToListAsync(clubListDtoQueryable)
            };
        }

        [Authorize(SnookerPermissions.Players.Default)]
        public virtual async Task<PagedResultDto<ClubPlayerListDto>> GetPlayersListAsync(Guid id, GetClubPlayersInput input)
        {
            long totalCount = await _clubPlayerRepository.GetCountAsync(
                input.FilterText,
                id,
                null,
                input.IsPrimaryClubOfPlayer);

            IQueryable<ClubPlayer> clubPlayerQueryable = await _clubPlayerRepository.GetFilteredQueryableAsync(
                input.FilterText,
                id,
                null,
                input.IsPrimaryClubOfPlayer,
                input.Sorting,
                input.MaxResultCount);

            IQueryable<Player> playerQueryable = await _playerRepository.GetFilteredQueryableAsync(input.FilterText);

            IQueryable<ClubPlayerWithNavigationProperties> clubPlayerWithNavigationPropertiesQueryable = clubPlayerQueryable.Join(
                playerQueryable,
                x => x.PlayerId,
                x => x.Id,
                (clubPlayer, player) => new ClubPlayerWithNavigationProperties()
                {
                    Id = clubPlayer.Id,
                    ClubId = clubPlayer.ClubId,
                    PlayerId = clubPlayer.PlayerId,
                    Player = player,
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

        [Authorize(SnookerPermissions.Clubs.Edit)]
        public virtual async Task<ClubDto> UpdateAsync(Guid id, ClubUpdateDto input)
        {
            Club club = await _clubRepository.GetAsync(id);

            club.Name = input.Name;

            club = await _clubRepository.UpdateAsync(club, autoSave: true);

            return ObjectMapper.Map<Club, ClubDto>(club);
        }
    }
}