using Volo.Abp.Reflection;

namespace Snooker.Platform.Permissions;

public class PlatformPermissions
{
    public const string GroupName = "Platform";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PlatformPermissions));
    }
}
