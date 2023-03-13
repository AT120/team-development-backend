using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamDevelopmentBackend.Model
{
    public class UserDbModel
    {
        [Key]
        public Guid Id { get; set; }
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid? DefaultFilterId { get; set; }
    }
}
