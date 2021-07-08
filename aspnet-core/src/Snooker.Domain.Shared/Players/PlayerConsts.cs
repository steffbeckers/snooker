namespace Snooker.Players
{
    public static class PlayerConsts
    {
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;

        private const string DefaultSorting = "{0}FirstName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Player." : string.Empty);
        }
    }
}