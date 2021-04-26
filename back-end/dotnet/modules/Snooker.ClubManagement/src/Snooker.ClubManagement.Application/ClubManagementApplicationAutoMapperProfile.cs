using AutoMapper;
using Snooker.ClubManagement.Clubs;
using Snooker.ClubManagement.Clubs.Dto;
using Volo.Abp.AutoMapper;

namespace Snooker.ClubManagement
{
    public class ClubManagementApplicationAutoMapperProfile : Profile
    {
        public ClubManagementApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Club, ClubDto>();
            CreateMap<CreateClubDto, Club>()
                .ForMember(x => x.ExtraProperties, x => x.Ignore())
                .ForMember(x => x.ConcurrencyStamp, x => x.Ignore())
                .IgnoreFullAuditedObjectProperties();
            CreateMap<UpdateClubDto, Club>()
                .ForMember(x => x.ExtraProperties, x => x.Ignore())
                .ForMember(x => x.ConcurrencyStamp, x => x.Ignore())
                .IgnoreFullAuditedObjectProperties();
        }
    }
}