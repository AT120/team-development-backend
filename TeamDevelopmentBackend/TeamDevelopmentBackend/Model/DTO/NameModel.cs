using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO;

public class NameModel
{
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}