using Snooker.ClubManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Snooker.ClubManagement.Permissions
{
    public class ClubManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var clubManagementGroup = context.AddGroup(ClubManagementPermissions.GroupName, L("Permission:ClubManagement"));

            // Clubs
            PermissionDefinition clubsPermission = clubManagementGroup.AddPermission(ClubManagementPermissions.Clubs, L("Permission:Clubs"), MultiTenancySides.Both);
            clubsPermission.AddChild(ClubManagementPermissions.CreateClubs, L("Permission:Clubs:Create"), MultiTenancySides.Both);
            clubsPermission.AddChild(ClubManagementPermissions.EditClubs, L("Permission:Clubs:Edit"), MultiTenancySides.Both);
            clubsPermission.AddChild(ClubManagementPermissions.DeleteClubs, L("Permission:Clubs:Delete"), MultiTenancySides.Both);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ClubManagementResource>(name);
        }
    }
}