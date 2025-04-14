using Microsoft.EntityFrameworkCore;
using MyTasks01.Helper;
using MyTasks01.Interfaces;

namespace MyTasks01.Services
{
    public class BaseService<T> : IGenericeInterface<T> where T : class
    {
        private readonly MyTasksDBContext _context;

        public BaseService(MyTasksDBContext context)
        {
            _context = context;
        }
        public virtual async Task Add(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task Delete(int id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
