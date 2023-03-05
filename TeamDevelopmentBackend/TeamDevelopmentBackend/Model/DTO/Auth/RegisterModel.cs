using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamDevelopmentBackend.Model.DTO.Auth;

public class RegisterModel
{
    
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}