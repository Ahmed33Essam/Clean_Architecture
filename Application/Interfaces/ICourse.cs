namespace Application.Interfaces
{
    public interface ICourse
    {
        public void Add(Course obj);
        public void Remove(int id);
        public void Update(Course obj);
        public Course GetByID(int id);
        public List<Course> GetAll();
        public void Save();
    }
}
