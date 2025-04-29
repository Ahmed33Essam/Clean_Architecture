namespace Application.Interfaces
{
    public interface IUser<T> where T : class
    {
        public void Add(T obj);
        public void Remove(int id);
        public void Update(T obj);
        public T GetByID(int id);
        public List<T> GetAll();
        public void Save();
    }
}
