namespace EduQuest.Repositorys
{
    public class QuestionRepo
    {
        private readonly EduQuestContext context;
        public QuestionRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Question obj)
        {
            context.Add(obj);
        }

        public List<Question> GetAll()
        {
            return context.Questions.ToList();
        }

        public Question GetByID(int id)
        {
            return context.Questions.Find(id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(Question obj)
        {
            context.Update(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
