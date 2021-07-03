using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Snooker.Clubs
{
    public interface IClubsAppService : IApplicationService
    {
        Task<ClubDto> CreateAsync(ClubCreateDto input);

        Task DeleteAsync(Guid id);

        Task<ClubDto> GetAsync(Guid id);

        Task<PagedResultDto<ClubDto>> GetListAsync(GetClubsInput input);

        Task<ClubDto> UpdateAsync(Guid id, ClubUpdateDto input);
    }
}