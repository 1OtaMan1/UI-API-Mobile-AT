using API.Interfaces;
using API.Models.MailTrap;
using API.Rest;
using Newtonsoft.Json;
using RestSharp;

namespace API.Mailtrap;

public class MailtrapApiClient
{
#pragma warning disable S1075 // URIs should not be hardcoded
    private const string BaseUrl = "https://mailtrap.io";
#pragma warning restore S1075 // URIs should not be hardcoded
    private readonly IRequestManager _requestManager;
        
    public MailtrapApiClient()
    {
        _requestManager = new RequestManager(RestClientTypes.Mailtrap, baseUrl: BaseUrl);
    }

    public List<MailtrapInboxApiModel> GetInboxes()
    {
        var response = _requestManager.SendRequest(Method.GET, "api/v1/inboxes");
        return JsonConvert.DeserializeObject<List<MailtrapInboxApiModel>>(response.Content);
    }

    public MailtrapInboxApiModel CreateInbox(string name)
    {
        var response = _requestManager.SendRequest(Method.POST, "api/v1/companies/71826/inboxes", new { inbox = new { name } });
        return JsonConvert.DeserializeObject<MailtrapInboxApiModel>(response.Content);
    }

    public void DeleteInbox(string inboxId)
    {
        _requestManager.SendRequest(Method.DELETE, $"/api/v1/inboxes/{inboxId}");
    }

    public void ToggleEmailForInbox(string inboxId)
    {
        _requestManager.SendRequest(Method.PATCH, $"/api/v1/inboxes/{inboxId}/toggle_email_username");
    }

    public MailtrapInboxApiModel GetInbox(string inboxId)
    {
        var response = _requestManager.SendRequest(Method.GET, $"api/v1/inboxes/{inboxId}");
        return JsonConvert.DeserializeObject<MailtrapInboxApiModel>(response.Content);
    }

    public List<MailtrapMessageApiModel> GetMessages(string inboxId, string search = null, int page = 1)
    {
        var request = new RestRequest($"api/v1/inboxes/{inboxId}/messages", Method.GET);

        if (search != null)
        {
            request.AddQueryParameter("search", search);
        }

        request.AddQueryParameter("page", page.ToString());

        var response = _requestManager.SendRequest(request);
        return JsonConvert.DeserializeObject<List<MailtrapMessageApiModel>>(response.Content);
    }

    public MailtrapMessageApiModel GetMessage(string inboxId, string messageId)
    {
        var response = _requestManager.SendRequest(Method.GET, $"api/v1/inboxes/{inboxId}/messages/{messageId}");
        return JsonConvert.DeserializeObject<MailtrapMessageApiModel>(response.Content);
    }

    public string GetMessageTextBody(string inboxId, string messageId)
    {
        var response = _requestManager.SendRequest(Method.GET, $"api/v1/inboxes/{inboxId}/messages/{messageId}/body.txt");
        return response.Content;
    }

    public string GetMessageHtmlBody(string inboxId, string messageId)
    {
        var response = _requestManager.SendRequest(Method.GET, $"api/v1/inboxes/{inboxId}/messages/{messageId}/body.html");
        return response.Content;
    }

    public string GetMessageRawHtmlBody(string inboxId, string messageId)
    {
        var response = _requestManager.SendRequest(Method.GET, $"api/v1/inboxes/{inboxId}/messages/{messageId}/body.htmlsource");
        return response.Content;
    }

    public string GetMessageRawBody(string inboxId, string messageId)
    {
        var response = _requestManager.SendRequest(Method.GET, $"api/v1/inboxes/{inboxId}/messages/{messageId}/body.raw");
        return response.Content;
    }

    public void DeleteMessage(string inboxId, string messageId)
    {
        _requestManager.SendRequest(Method.DELETE, $"api/v1/inboxes/{inboxId}/messages/{messageId}");
    }

    public void DeleteAllMessages(string inboxId)
    {
        _requestManager.SendRequest(Method.PATCH, $"api/v1/inboxes/{inboxId}/clean");
    }
}