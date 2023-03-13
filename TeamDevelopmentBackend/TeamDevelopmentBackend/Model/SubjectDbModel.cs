using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model
{

    public class SubjectDbModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}


