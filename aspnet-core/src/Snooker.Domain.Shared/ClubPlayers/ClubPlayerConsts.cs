namespace Snooker.ClubPlayers
{
    public static class ClubPlayerConsts
    {
        private const string DefaultSorting = "{0}ClubId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ClubPlayer." : string.Empty);
        }
    }
}