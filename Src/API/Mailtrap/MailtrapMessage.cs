using API.Models.MailTrap;

namespace API.Mailtrap;

public class MailtrapMessage
{
    public MailtrapMessageApiModel Properties { get; set; } = new();

    public string Text { get; set; }
}