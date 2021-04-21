using Snooker.ClubManagement.Localization;
using Volo.Abp.Application.Services;

namespace Snooker.ClubManagement
{
    public abstract class ClubManagementAppService : ApplicationService
    {
        protected ClubManagementAppService()
        {
            LocalizationResource = typeof(ClubManagementResource);
            ObjectMapperContext = typeof(ClubManagementApplicationModule);
        }
    }
}
