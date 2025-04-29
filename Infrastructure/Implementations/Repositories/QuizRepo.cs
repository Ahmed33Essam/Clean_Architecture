using Microsoft.EntityFrameworkCore;

namespace EduQuest.Repositorys
{
    public class QuizRepo : IQuiz
    {
        private readonly EduQuestContext context;
        public QuizRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Quiz obj)
        {
            context.Add(obj);
        }

        public List<Quiz> GetAll()
        {
            return context.Quizzes.ToList();
        }

        public Quiz GetByID(int id)
        {
            return context.Quizzes.Find(id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(Quiz obj)
        {
            context.Update(obj);
        }

        public void AddQuestion(int quizId, Question question)
        {
            if (question == null) return;

            var quiz = context.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.Id == quizId);
            if (quiz == null) return;

            quiz.Questions ??= new List<Question>();

            context.Questions.Add(question);
            quiz.Questions.Add(question);

            context.SaveChanges();
        }


        public void RemoveQuestion(int quizId, int questionId)
        {
            var quiz = context.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.Id == quizId);
            if (quiz == null || quiz.Questions == null)
            {
                Console.WriteLine("Quiz not found or questions are null.");
                return;
            }

            var question = quiz.Questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                Console.WriteLine("Question not found.");
                return;
            }

            quiz.Questions.Remove(question);
            context.Entry(question).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void EditQuestion(Question question)
        {
            context.Questions.Update(question);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
