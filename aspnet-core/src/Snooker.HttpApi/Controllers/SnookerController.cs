using Snooker.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Snooker.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SnookerController : AbpControllerBase
{
    protected SnookerController()
    {
        LocalizationResource = typeof(SnookerResource);
    }
}
