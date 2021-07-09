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

            CreateMap<Club, ClubDto>();
            CreateMap<Club, ClubListDto>();

            CreateMap<Player, PlayerDto>();
            CreateMap<Player, PlayerListDto>();
        }
    }
}