using AutoMapper;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Seasons;

namespace Snooker.Interclub;

public class InterclubApplicationAutoMapperProfile : Profile
{
    public InterclubApplicationAutoMapperProfile()
    {
        CreateMap<Division, SeasonDivisionDto>();

        CreateMap<Season, SeasonDto>();
    }
}