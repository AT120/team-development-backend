using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.User;

public class UserInfoModel
{
    [JsonPropertyName("role")]
    public Role Role { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("defaultId")]
    public Guid DefaultId { get; set; }
}