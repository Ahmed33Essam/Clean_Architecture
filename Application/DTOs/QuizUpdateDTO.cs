namespace Application.DTOs
{
    public class QuizUpdateDTO
    {
        public int Id { get; set; }
        public string LessonName { get; set; }
        public string Subject { get; set; }
        public TimeSpan Duration { get; set; }
        public int InstractorId { get; set; }
    }
}
