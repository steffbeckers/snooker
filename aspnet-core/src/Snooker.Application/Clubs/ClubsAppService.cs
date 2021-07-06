using Microsoft.AspNetCore.Authorization;
using Snooker.Permissions;
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
        private readonly IClubRepository _clubRepository;

        public ClubsAppService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
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

        public virtual async Task<PagedResultDto<ClubSimpleDto>> GetListAsync(GetClubsInput input)
        {
            //long totalCount = await _clubRepository.GetCountAsync(input.FilterText, input.Name);
            //List<Club> clubs = await _clubRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            //return new PagedResultDto<ClubDto>
            //{
            //    TotalCount = totalCount,
            //    Items = ObjectMapper.Map<List<Club>, List<ClubDto>>(clubs)
            //};

            long totalCount = await _clubRepository.GetCountAsync(input.FilterText, input.Name);
            IQueryable<Club> clubQueryable = await _clubRepository.GetFilteredQueryableAsync(
                input.FilterText,
                input.Name,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);
            IQueryable<ClubSimpleDto> clubDtoQueryable = ObjectMapper.GetMapper().ProjectTo<ClubSimpleDto>(clubQueryable);

            return new PagedResultDto<ClubSimpleDto>
            {
                TotalCount = totalCount,
                Items = await AsyncExecuter.ToListAsync(clubDtoQueryable)
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