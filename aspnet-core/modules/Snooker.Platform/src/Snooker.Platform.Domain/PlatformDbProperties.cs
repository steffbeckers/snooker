namespace Snooker.Platform;

public static class PlatformDbProperties
{
    public const string ConnectionStringName = "Default";

    public static string? DbSchema { get; set; } = null;

    public static string DbTablePrefix { get; set; } = "Platform";
}