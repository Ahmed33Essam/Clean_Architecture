namespace Application.Interfaces
{
    public interface IQuestionBank
    {
        public void AddQuestion(int questionId, string questionSubject);

        public void RemoveQuestion(int questionId, string questionSubject);

        public List<int> getQuestions(string questionSubject);

        public void Add(QuestionBank obj);

        public List<QuestionBank> GetAll();

        public QuestionBank GetByID(int id);

        public void Remove(int id);

        public void Update(QuestionBank obj);

        public void Save();
    }
}
