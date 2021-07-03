namespace Snooker.Permissions
{
    public static class SnookerPermissions
    {
        public const string GroupName = "Snooker";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public class Clubs
        {
            public const string Default = GroupName + ".Clubs";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
        }
    }
}