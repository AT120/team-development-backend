using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.User;

public class GroupEditModel
{
    [JsonPropertyName("groupId")]
    public Guid GroupId { get; set; }
}