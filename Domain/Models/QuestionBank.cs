namespace Domain.Models
{
    public class QuestionBank
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public List<int>? questions { set; get; } = new List<int>();
    }
}
