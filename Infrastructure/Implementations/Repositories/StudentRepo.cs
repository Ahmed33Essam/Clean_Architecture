
namespace EduQuest.Repositorys
{
    public class StudentRepo : IUser<Student>
    {
        private readonly EduQuestContext context;
        public StudentRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Student obj)
        {
            context.Add(obj);
        }

        public List<Student> GetAll()
        {
            return context.Students.ToList();
        }

        public Student GetByID(int id)
        {
            return context.Students.Find(id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(Student obj)
        {
            context.Update(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
