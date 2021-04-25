using Volo.Abp.Reflection;

namespace Snooker.ClubManagement.Permissions
{
    public class ClubManagementPermissions
    {
        public const string GroupName = "ClubManagement";

        // Clubs
        public const string Clubs = GroupName + ".Clubs";
        public const string CreateClubs = Clubs + ".Create";
        public const string EditClubs = Clubs + ".Update";
        public const string DeleteClubs = Clubs + ".Delete";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ClubManagementPermissions));
        }
    }
}