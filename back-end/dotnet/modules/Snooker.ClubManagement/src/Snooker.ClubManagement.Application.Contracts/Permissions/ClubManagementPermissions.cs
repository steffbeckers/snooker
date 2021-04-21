using Volo.Abp.Reflection;

namespace Snooker.ClubManagement.Permissions
{
    public class ClubManagementPermissions
    {
        public const string GroupName = "ClubManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ClubManagementPermissions));
        }
    }
}