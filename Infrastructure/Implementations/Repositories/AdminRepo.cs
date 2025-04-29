namespace EduQuest.Repositorys
{
    public class AdminRepo : IUser<Admin>
    {
        private readonly EduQuestContext context;
        public AdminRepo(EduQuestContext context)
        {
            this.context = context;
        }
        public void Add(Admin obj)
        {
            context.Add(obj);
        }
        public List<Admin> GetAll()
        {
            return context.Admins.ToList();
        }
        public Admin GetByID(int id)
        {
            return context.Admins.Find(id);
        }
        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }
        public void Update(Admin obj)
        {
            context.Update(obj);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
