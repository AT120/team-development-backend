using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model
{
    public class TeacherDbModel
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
