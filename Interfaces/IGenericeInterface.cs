namespace MyTasks01.Interfaces
{
    public interface IGenericeInterface<T> where T : class
    {
        public Task Add(T entity);
        public Task Delete(int id);
        public Task<T> GetById(int id);
        public Task Update(T entity);
        public Task<List<T>> GetAll();
    }
}
