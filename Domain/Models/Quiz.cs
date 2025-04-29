using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string LessonName { get; set; }
        public string Subject { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Question>? Questions { get; set; } = new List<Question>();


        [ForeignKey("Instractor")]
        public int InstractorId { get; set; }
        [JsonIgnore]
        public Instructor? Instractor { get; set; }
    }
}
