using Snooker.Interclub.Localization;
using Volo.Abp.Application.Services;

namespace Snooker.Interclub;

public abstract class InterclubAppService : ApplicationService
{
    protected InterclubAppService()
    {
        LocalizationResource = typeof(InterclubResource);
        ObjectMapperContext = typeof(InterclubApplicationModule);
    }
}
