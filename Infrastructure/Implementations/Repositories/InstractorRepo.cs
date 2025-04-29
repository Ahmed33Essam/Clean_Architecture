namespace EduQuest.Repositorys
{
    public class InstractorRepo : IUser<Instructor>
    {
        private readonly EduQuestContext context;
        public InstractorRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Instructor obj)
        {
            context.Add(obj);
        }
        public List<Instructor> GetAll()
        {
            return context.Instructors.ToList();
        }
        public Instructor GetByID(int id)
        {
            return context.Instructors.Find(id);
        }
        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }
        public void Update(Instructor obj)
        {
            context.Update(obj);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
