namespace EduQuest.Repositorys
{
    public class QuestionBankRepo : IQuestionBank
    {
        private readonly EduQuestContext context;
        public QuestionBankRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void AddQuestion(int questionId, string questionSubject)
        {
            QuestionBank? questionBank = context.QuestionBanks.FirstOrDefault(x => x.Subject == questionSubject);
            if (questionBank == null)
            {
                questionBank = new QuestionBank
                {
                    Subject = questionSubject,
                    questions = new List<int> { questionId }
                };
                context.Add(questionBank);
            }
            else
            {
                if (questionBank.questions == null)
                    questionBank.questions = new List<int>();

                questionBank.questions.Add(questionId);
                context.Update(questionBank);
            }

        }

        public void RemoveQuestion(int questionId, string questionSubject)
        {
            QuestionBank? questionBank = context.QuestionBanks.FirstOrDefault(x => x.Subject == questionSubject);
            if (questionBank != null)
            {
                questionBank.questions.Remove(questionId);
                context.Update(questionBank);
            }
        }

        public List<int> getQuestions(string questionSubject)
        {
            QuestionBank? questionBank = context.QuestionBanks.FirstOrDefault(x => x.Subject == questionSubject);
            if (questionBank != null)
            {
                return questionBank.questions;
            }
            return null;
        }

        public void Add(QuestionBank obj)
        {
            context.Add(obj);
        }

        public List<QuestionBank> GetAll()
        {
            return context.QuestionBanks.ToList();
        }

        public QuestionBank GetByID(int id)
        {
            return context.QuestionBanks.Find(id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(QuestionBank obj)
        {
            context.Update(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
