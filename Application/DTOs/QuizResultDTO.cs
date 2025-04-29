namespace Application.DTOs
{
    public class QuizResultDTO
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double ScorePercentage { get; set; }
    }
}
