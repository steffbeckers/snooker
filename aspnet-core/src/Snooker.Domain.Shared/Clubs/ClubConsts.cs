namespace Snooker.Clubs
{
    public static class ClubConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Club." : string.Empty);
        }

        public const int NameMaxLength = 100;
    }
}