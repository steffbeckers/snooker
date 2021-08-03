namespace Snooker
{
    public static class SnookerDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */

        public static class ClubPlayers
        {
            public const string PlayerAlreadyLinkedToClub = _prefix + nameof(PlayerAlreadyLinkedToClub);
            private const string _prefix = nameof(ClubPlayers) + ":";
        }

        public static class Samples
        {
            public const string SampleError = _prefix + nameof(SampleError);
            private const string _prefix = nameof(Samples) + ":";
        }
    }
}