namespace Application.DTOs
{
    public class QuizSubmissionDTO
    {
        public int QuizId { get; set; }
        public List<StudentAnswerDTO> Answers { get; set; } = new();
    }

    public class StudentAnswerDTO
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }

}
