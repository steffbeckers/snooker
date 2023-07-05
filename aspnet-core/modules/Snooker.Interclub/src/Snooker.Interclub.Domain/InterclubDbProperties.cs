namespace Snooker.Interclub;

public static class InterclubDbProperties
{
    public static string DbTablePrefix { get; set; } = "Interclub";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Interclub";
}
