using Snooker.Platform.Localization;
using Volo.Abp.Application.Services;

namespace Snooker.Platform;

public abstract class PlatformAppService : ApplicationService
{
    protected PlatformAppService()
    {
        LocalizationResource = typeof(PlatformResource);
        ObjectMapperContext = typeof(PlatformApplicationModule);
    }
}
