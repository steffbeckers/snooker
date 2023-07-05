using Snooker.Interclub.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Snooker.Interclub;

public abstract class InterclubController : AbpControllerBase
{
    protected InterclubController()
    {
        LocalizationResource = typeof(InterclubResource);
    }
}
