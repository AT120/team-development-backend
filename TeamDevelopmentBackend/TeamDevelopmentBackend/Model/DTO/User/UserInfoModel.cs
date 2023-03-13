using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.User;

public class UserInfoModel
{
    public string Name { get; set; }
    public Role Role { get; set; }
    public string Login { get; set; }
    public Guid? DefaultId { get; set; }
}