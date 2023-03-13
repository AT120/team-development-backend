using System.ComponentModel.DataAnnotations;

namespace TeamDevelopmentBackend.Model.DTO.Auth;

public class LoginCredsModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}