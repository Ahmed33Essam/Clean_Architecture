using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Rate
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Value { get; set; }      //1-5
        public string TargetType { get; set; } //"Course", "Instructor"
        public int TargetId { get; set; }     // The ID of the course or instructor

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
