using Snooker.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Snooker.Permissions
{
    public class SnookerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            PermissionGroupDefinition snookerGroup = context.AddGroup(SnookerPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(SnookerPermissions.MyPermission1, L("Permission:MyPermission1"));

            PermissionDefinition clubPermission = snookerGroup.AddPermission(SnookerPermissions.Clubs.Default, L("Permission:Clubs"));
            clubPermission.AddChild(SnookerPermissions.Clubs.Create, L("Permission:Create"));
            clubPermission.AddChild(SnookerPermissions.Clubs.Edit, L("Permission:Edit"));
            clubPermission.AddChild(SnookerPermissions.Clubs.Delete, L("Permission:Delete"));

            PermissionDefinition playerPermission = snookerGroup.AddPermission(SnookerPermissions.Players.Default, L("Permission:Players"));
            playerPermission.AddChild(SnookerPermissions.Players.Create, L("Permission:Create"));
            playerPermission.AddChild(SnookerPermissions.Players.Edit, L("Permission:Edit"));
            playerPermission.AddChild(SnookerPermissions.Players.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SnookerResource>(name);
        }
    }
}