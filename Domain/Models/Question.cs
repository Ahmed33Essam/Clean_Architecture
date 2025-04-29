using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionHead { get; set; }
        public string Choose_1 { get; set; }
        public string Choose_2 { get; set; }
        public string Choose_3 { get; set; }
        public string Choose_4 { get; set; }
        public string RightOne { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        [JsonIgnore]
        public Quiz? Quiz { get; set; }
    }
}
