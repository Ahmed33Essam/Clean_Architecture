namespace Application.DTOs
{
    public class CourseUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public DateTime? DiscountDate { get; set; }
        public TimeSpan Duration { get; set; }
        public int LessonsNumber { get; set; }
        public int Level { get; set; }
        public int? StudentsNumber { get; set; }
        public List<string> Goals { get; set; } = new();
        public int InstractorId { get; set; }
    }
}
