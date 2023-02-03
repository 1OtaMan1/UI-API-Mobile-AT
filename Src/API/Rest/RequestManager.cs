using System.Diagnostics;
using System.Net;
using API.Exceptions;
using API.Interfaces;
using Core.EnvironmentSettings;
using Newtonsoft.Json;
using RestSharp;

namespace API.Rest;

public class RequestManager : IRequestManager
{
    private readonly RestClientTypes _type;
    private IRestClient _client;

    private CredentialsStorage _credentials;
    private readonly string _baseUrl;

    static RequestManager()
    {
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
#pragma warning disable S3257 // Declarations and initializations should be as concise as possible
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#pragma warning restore S3257 // Declarations and initializations should be as concise as possible
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections
    }

    public RequestManager(RestClientTypes type, CredentialsStorage credentials = null, string baseUrl = "")
    {
        _credentials = credentials;
        _baseUrl = baseUrl;
        _type = type;
    }

    public IRestResponse SendRequest(Method method, string endPoint, object body = null, Dictionary<string, string> header = null)
    {
        var request = PrepareRequest(method, endPoint, body, header);
        return SendRequest(request);
    }

    public IRestResponse SendRequest(IRestRequest request)
    {
        _credentials ??= new CredentialsStorage { FirstName = "Undefined", LastName = "User", Id = Guid.Empty }; // Made here to prevent API call failing
        _client = RestClientFactory.Create(_type, _credentials, _baseUrl);
        return Execute(request);
    }

    public byte[] DownloadData(IRestRequest request)
    {
        _client = RestClientFactory.Create(_type, _credentials, _baseUrl);
        return _client.DownloadData(request);
    }

    private static IRestRequest PrepareRequest(Method method, string endPoint, object body, Dictionary<string, string> header)
    {
        var request = new RestRequest(endPoint, method);
        request.AddHeader("cache-control", "no-cache");
        request.AddHeader("Content-Type", "application/json");
        if (header != null)
        {
            request.AddHeader(header.Keys.First(), header.Values.First());
        }

        if (body != null)
        {
            request.AddJsonBody(body);
        }

        return request;
    }

    private IRestResponse Execute(IRestRequest request)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var response = _client.Execute(request);
        stopWatch.Stop();

        Trace.TraceInformation("\n\n" +
                               "Request finished at: " + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss':'ffff") + "\n" +
                               "Path: " + response.ResponseUri + "\n" +
                               "Method: " + request.Method + "\n" +
                               "Status code: " + response.StatusCode + "\n" +
                               "Requester: " + _credentials.FullName + " " + _credentials.Id + "\n" +
                               "Request time: " + stopWatch.ElapsedMilliseconds + " ms\n\n");

        if (response.IsSuccessful)
        {
            return response;
        }

        LogRequest(request, response, stopWatch.ElapsedMilliseconds);
        ResponseUnsuccessful(response);

        return response;
    }

    private void LogRequest(IRestRequest request, IRestResponse response, long durationMs)
    {
        var requestToLog = new
        {
            uri = _client.BuildUri(request),
            resource = request.Resource,
            method = request.Method.ToString(),
            requester = _credentials.FullName + " " + _credentials.Id,
            parameters = request.Parameters.Select(parameter => new
            {
                name = parameter.Name,
                value = parameter.Value,
                type = parameter.Type.ToString()
            })
        };

        var responseToLog = new
        {
            statusCode = response.StatusCode,
            responseUri = response.ResponseUri,
            requester = _credentials.FullName + " " + _credentials.Id,
            content = response.Content,
            headers = response.Headers,
            errorMessage = response.ErrorMessage
        };

        Trace.TraceError(
            "\n\n" +
            $" \n\nRequest failed at: {DateTime.Now}," +
            $" \n\nRequest completed in {durationMs} ms," +
            $" \n\nRequest: {JsonConvert.SerializeObject(requestToLog, Formatting.Indented)}" +
            $" \n\nResponse: {JsonConvert.SerializeObject(responseToLog, Formatting.Indented)}");
    }

    private static void ResponseUnsuccessful(IRestResponse response)
    {
        var exception = new UnsuccessfulResponseException();
        exception.Data.Add("statusCode", response.StatusCode.ToString());
        exception.Data.Add("content", response.Content);

        throw exception;
    }
}