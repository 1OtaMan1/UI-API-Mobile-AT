using RestSharp;
using RestSharp.Serialization.Json;

namespace Core.Extensions;

public static class ResponseExtensions
{
    public static T As<T>(this IRestResponse response)
    {
        return new JsonDeserializer().Deserialize<T>(response);
    }

    public static Guid ExtractGuid(this IRestResponse response)
    {
        return new Guid(response.Content.Split('\"')[1]);
    }
}