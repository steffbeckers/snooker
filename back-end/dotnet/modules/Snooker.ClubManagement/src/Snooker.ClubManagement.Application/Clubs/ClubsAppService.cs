using Microsoft.AspNetCore.Authorization;
using Snooker.ClubManagement.Clubs.Dto;
using Snooker.ClubManagement.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Snooker.ClubManagement.Clubs
{
    [Authorize(ClubManagementPermissions.Clubs)]
    public class ClubsAppService :
        CrudAppService<
            Club,
            ClubDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateClubDto,
            UpdateClubDto
        >,
        IClubsAppService
    {
        private readonly IRepository<Club, Guid> _clubRepository;
        private readonly ClubManager _clubManager;

        public ClubsAppService(
            IRepository<Club, Guid> clubRepository,
            ClubManager clubManager
        ) : base(clubRepository)
        {
            _clubRepository = clubRepository;
            _clubManager = clubManager;
            GetListPolicyName = ClubManagementPermissions.Clubs;
            GetPolicyName = ClubManagementPermissions.Clubs;
            CreatePolicyName = ClubManagementPermissions.CreateClubs;
            UpdatePolicyName = ClubManagementPermissions.EditClubs;
            DeletePolicyName = ClubManagementPermissions.DeleteClubs;
        }

        [Authorize(ClubManagementPermissions.CreateClubs)]
        public override async Task<ClubDto> CreateAsync(CreateClubDto input)
        {
            Club club = await _clubManager.CreateAsync(
                input.Name
            );

            await _clubRepository.InsertAsync(club);

            return ObjectMapper.Map<Club, ClubDto>(club);
        }
    }
}
