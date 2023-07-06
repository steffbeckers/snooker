namespace Snooker.Platform;

public static class PlatformDbProperties
{
    public static string DbTablePrefix { get; set; } = "Platform";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Platform";
}
