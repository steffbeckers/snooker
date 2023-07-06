using Snooker.Platform.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Snooker.Platform;

public abstract class PlatformController : AbpControllerBase
{
    protected PlatformController()
    {
        LocalizationResource = typeof(PlatformResource);
    }
}
