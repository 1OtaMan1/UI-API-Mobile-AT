using Newtonsoft.Json;

namespace API.Models.MailTrap;

public class MailtrapInboxApiModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "username")]
    public string UserName { get; set; }

    [JsonProperty(PropertyName = "password")]
    public string Password { get; set; }

    [JsonProperty(PropertyName = "email_username")]
    public string EmailUsername { get; set; }

    [JsonIgnore]
    public string Email => EmailUsername + "@inbox.mailtrap.io";
}