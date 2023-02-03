using Newtonsoft.Json;

namespace API.Models.MailTrap;

public class MailtrapMessageApiModel
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName = "inbox_id")]
    public string InboxId { get; set; }

    [JsonProperty(PropertyName = "subject")]
    public string Subject { get; set; }

    [JsonProperty(PropertyName = "sent_at")]
    public DateTime SentAt { get; set; }

    [JsonProperty(PropertyName = "from_email")]
    public string FromEmail { get; set; }

    [JsonProperty(PropertyName = "to_email")]
    public string ToEmail { get; set; }
}