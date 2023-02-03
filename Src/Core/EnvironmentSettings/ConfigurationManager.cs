using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.EnvironmentSettings;

public static class ConfigurationManager
{
    public static IConfiguration AppSettings { get; }

    static ConfigurationManager()
    {
        string env;
        var jsonFileName = "appsettings.json";
        try
        {
            var json = File.ReadAllText($"{Directory.GetCurrentDirectory()}//{jsonFileName}");
            var jsonObject = (JObject) JsonConvert.DeserializeObject(json);
            env = jsonObject["Environment"].Value<string>();
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            throw;
        }

        if (env == "$appsettings.at.environment.value$")
        {
            env = "dev";
            jsonFileName = $"appsettings.{env}.json";
        }

        AppSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(jsonFileName)
            .Build();
    }
}