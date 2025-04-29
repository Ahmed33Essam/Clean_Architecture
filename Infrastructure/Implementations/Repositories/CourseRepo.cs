
namespace EduQuest.Repositorys
{
    public class CourseRepo : ICourse
    {
        private readonly EduQuestContext context;

        public CourseRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Course obj)
        {
            context.Add(obj);
        }

        public List<Course> GetAll()
        {
            return context.Courses.ToList();
        }

        public Course GetByID(int id)
        {
            return context.Courses.Find(id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(Course obj)
        {
            context.Update(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
