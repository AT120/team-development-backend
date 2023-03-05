using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TeamDevelopmentBackend.Model;

public class CounterStorageDbModel
{
    [Key]
    public ulong Last { get; set; } 
}