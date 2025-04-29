using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string DocumentUrl { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
