using Snooker.Interclub.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Snooker.Interclub.Permissions;

public class InterclubPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InterclubPermissions.GroupName, L("Permission:Interclub"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InterclubResource>(name);
    }
}
