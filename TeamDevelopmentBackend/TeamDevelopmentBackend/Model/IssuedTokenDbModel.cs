using System.ComponentModel.DataAnnotations;

namespace TeamDevelopmentBackend.Model;

public class IssuedTokenDbModel
{
    [Key]
    public ulong RefreshTokenId { get; set; }
}