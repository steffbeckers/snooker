namespace Snooker.ClubManagement
{
    public static class ClubManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "ClubManagement";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "ClubManagement";
    }
}
