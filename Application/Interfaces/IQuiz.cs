namespace Application.Interfaces
{
    public interface IQuiz
    {
        public void Add(Quiz obj);

        public List<Quiz> GetAll();

        public Quiz GetByID(int id);

        public void Remove(int id);

        public void Update(Quiz obj);

        public void AddQuestion(int quizId, Question question);

        public void RemoveQuestion(int quizId, int questionId);

        public void EditQuestion(Question question);

        public void Save();
    }
}
