namespace API.Models.Auth;

public class LoggedUserApiModel
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Token { get; set; }
}