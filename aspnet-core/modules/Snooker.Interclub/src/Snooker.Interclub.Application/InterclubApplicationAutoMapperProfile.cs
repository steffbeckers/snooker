using AutoMapper;
using Snooker.Interclub.Divisions;
using Snooker.Interclub.Seasons;
using System.Linq;

namespace Snooker.Interclub;

public class InterclubApplicationAutoMapperProfile : Profile
{
    public InterclubApplicationAutoMapperProfile()
    {
        CreateMap<Division, SeasonDivisionDto>();

        CreateMap<Season, SeasonDto>()
            .ForMember(x => x.Divisions, x => x.MapFrom(y => y.Divisions.OrderBy(z => z.SortOrder)));
        CreateMap<Season, SeasonListDto>();
    }
}