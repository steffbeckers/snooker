using Snooker.Localization;
using Volo.Abp.Application.Services;

namespace Snooker
{
    // Inherit your application services from this class.
    public abstract class SnookerAppService : ApplicationService
    {
        protected SnookerAppService()
        {
            LocalizationResource = typeof(SnookerResource);
        }
    }
}