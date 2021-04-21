using Snooker.ClubManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Snooker.ClubManagement
{
    public abstract class ClubManagementController : AbpController
    {
        protected ClubManagementController()
        {
            LocalizationResource = typeof(ClubManagementResource);
        }
    }
}
