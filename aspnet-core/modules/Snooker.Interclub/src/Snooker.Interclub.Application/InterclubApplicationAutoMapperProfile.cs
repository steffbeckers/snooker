using AutoMapper;
using Snooker.Interclub.Seasons;

namespace Snooker.Interclub;

public class InterclubApplicationAutoMapperProfile : Profile
{
    public InterclubApplicationAutoMapperProfile()
    {
        CreateMap<Season, SeasonDto>();
    }
}