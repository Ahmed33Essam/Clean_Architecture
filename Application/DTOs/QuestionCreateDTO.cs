namespace Application.DTOs
{
    public class QuestionCreateDTO
    {
        public string QuestionHead { get; set; }
        public string Choose_1 { get; set; }
        public string Choose_2 { get; set; }
        public string Choose_3 { get; set; }
        public string Choose_4 { get; set; }
        public string RightOne { get; set; }
        public int QuizId { get; set; }
    }
}
