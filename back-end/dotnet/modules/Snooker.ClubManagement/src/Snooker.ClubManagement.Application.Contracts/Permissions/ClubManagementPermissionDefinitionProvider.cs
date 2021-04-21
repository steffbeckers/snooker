using Snooker.ClubManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Snooker.ClubManagement.Permissions
{
    public class ClubManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ClubManagementPermissions.GroupName, L("Permission:ClubManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ClubManagementResource>(name);
        }
    }
}