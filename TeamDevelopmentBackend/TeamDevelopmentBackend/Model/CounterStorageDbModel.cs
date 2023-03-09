using System.ComponentModel.DataAnnotations;

namespace TeamDevelopmentBackend.Model;

public class CounterStorageDbModel
{
    [Key]
    public int Id { get; set; }
    public ulong Last { get; set; } 
}