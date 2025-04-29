using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string Duration { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

    }
}
