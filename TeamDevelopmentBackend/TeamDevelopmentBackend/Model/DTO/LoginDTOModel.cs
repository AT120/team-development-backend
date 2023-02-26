using System.ComponentModel.DataAnnotations;

namespace TeamDevelopmentBackend.Model.DTO
{
    public class LoginDTOModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }
    }
}
