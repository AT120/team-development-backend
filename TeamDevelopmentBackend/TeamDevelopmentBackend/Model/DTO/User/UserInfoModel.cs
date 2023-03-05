using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.User;

public class UserInfoModel
{
    public Role Role { get; set; }
    public string Login { get; set; }
    public Guid DefaultId { get; set; }
}