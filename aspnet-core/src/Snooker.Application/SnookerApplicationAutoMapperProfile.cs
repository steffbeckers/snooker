using AutoMapper;
using Snooker.ClubPlayers;
using Snooker.Clubs;
using Snooker.Players;

namespace Snooker
{
    public class SnookerApplicationAutoMapperProfile : Profile
    {
        public SnookerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<ClubPlayer, ClubPlayerDto>();
            CreateMap<ClubPlayerWithNavigationProperties, ClubPlayerListDto>();
            CreateMap<ClubPlayerWithNavigationProperties, PlayerProfileDto>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Player.Id))
                .ForMember(x => x.FirstName, x => x.MapFrom(y => y.Player.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(y => y.Player.LastName));

            CreateMap<Club, ClubDto>();
            CreateMap<Club, ClubListDto>();
            CreateMap<Club, PlayerProfileClubDto>();

            CreateMap<Player, PlayerDto>();
            CreateMap<Player, PlayerListDto>();
        }
    }
}