namespace Snooker.Interclub;

public static class InterclubDbProperties
{
    public const string ConnectionStringName = "Default";

    public static string? DbSchema { get; set; } = null;

    public static string DbTablePrefix { get; set; } = "Interclub";
}