using Snooker.ClubManagement.Clubs.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Snooker.ClubManagement.Clubs
{
    public interface IClubsAppService : ICrudAppService<ClubDto, Guid, PagedAndSortedResultRequestDto, CreateClubDto, UpdateClubDto>
    {
    }
}
