using API.ApiUsers;
using Core.EnvironmentSettings;
using RestSharp;
using RestSharp.Authenticators;

namespace API.Rest;

public static class RestClientFactory
{
#pragma warning disable S1075 // URIs should not be hardcoded
    private const string MailServerBaseUrl = "https://mailtrap.io";
#pragma warning restore S1075 // URIs should not be hardcoded
    private const string MailServerToken = "7ce93414ea53912b530248c2d42a3e5b";

    public static IRestClient Create(RestClientTypes type, CredentialsStorage credentials, string baseUrl = "")
    {
        IRestClient client;

        switch (type)
        {
            case RestClientTypes.Mailtrap:
                client = JwtClient(MailServerBaseUrl, MailServerToken);
                break;
            case RestClientTypes.Base:
                client = BasicAuth(baseUrl);
                break;
            case RestClientTypes.Admin:
                credentials.Token ??= new TenantApiUser(credentials).Account.GetToken(credentials).Token;
                client = JwtClient(baseUrl, credentials.Token);
                break;

            default:
                throw new InvalidOperationException("Client type not supported.");
        }

        return client;
    }

    private static IRestClient JwtClient(string baseUrl, string token)
    {
        var client = new RestClient(baseUrl) { Authenticator = new JwtAuthenticator(token) };
        client.AddDefaultHeader("Authorization", token);
        return client;
    }

    private static IRestClient BasicAuth(string baseUrl)
    {
        var client = new RestClient(baseUrl);
        return client;
    }
}