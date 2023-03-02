using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.User
{
    public class LoginDTOModel
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("login")]
        public string Login { get; set; }
    }
}
