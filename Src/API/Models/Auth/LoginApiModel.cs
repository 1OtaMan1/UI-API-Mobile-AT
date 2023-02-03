using System.ComponentModel.DataAnnotations;

namespace API.Models.Auth;

public class LoginApiModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}