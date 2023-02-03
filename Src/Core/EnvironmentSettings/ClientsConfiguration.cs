namespace Core.EnvironmentSettings;

public static class ClientsConfiguration
{
    public const string LocalMachine1Name = "PF2L9TSL";

    public const string LocalMachine2Name = "GWH8YD3";

    public const string LocalMachine3Name = "Butcher";

    public static readonly List<string> LocalMachines = new() { LocalMachine1Name, LocalMachine2Name, LocalMachine3Name }; // made for exclude commenting on local machine. Able for extension
}