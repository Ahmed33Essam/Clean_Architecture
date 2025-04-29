using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Course
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string subject { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public DateTime? DiscountDate { get; set; }
        public TimeSpan Duration { get; set; }
        public int LessonsNumber { get; set; }
        public int Level { get; set; }
        public int? StudentsNumber { get; set; }
        public float? Rate { get; set; }
        public List<string> Goals { get; set; }
        public Image? Image { get; set; }
        public List<int>? Videos { get; set; }
        public List<int>? Documents { get; set; }
        public List<int>? Quizzs { get; set; }

        [ForeignKey("Instractor")]
        public int InstractorId { get; set; }
        [JsonIgnore]
        public Instructor? Instractor { get; set; }
    }
}