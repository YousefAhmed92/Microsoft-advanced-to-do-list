using Microsoft.EntityFrameworkCore;
using MyTasks01.Helper;
using MyTasks01.Models;

namespace MyTasks01.Services
{
    public class ListService : BaseService<Models.Lists>  
    {
        private readonly MyTasksDBContext _context;

        public ListService(MyTasksDBContext context) : base(context)
        {
            _context = context;
        }
        public override Task Delete(int id)
        {
            return Delete(id);
        }
        public async override Task Add(Lists entity)
        {
            await base.Add(entity);
        }
        public async Task<Models.Lists> GetById(int id)
        {
            return await _context.Lists.FirstOrDefaultAsync(l => l.Id == id);
        }
        public override Task<List<Lists>> GetAll()
        {
            return _context.Lists.ToListAsync();

            //return base.GetAll();
        }
    }
}
