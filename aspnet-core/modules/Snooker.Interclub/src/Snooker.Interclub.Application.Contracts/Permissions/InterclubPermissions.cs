using Volo.Abp.Reflection;

namespace Snooker.Interclub.Permissions;

public class InterclubPermissions
{
    public const string GroupName = "Interclub";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InterclubPermissions));
    }
}
