using MyTasks01.Helper;
using MyTasks01.Models;

namespace MyTasks01.Services
{
    public class SubTaskService : BaseService<Models.SubTask>
    {
        private readonly MyTasksDBContext _context;

        public SubTaskService(MyTasksDBContext context) : base(context) 
        {
            _context = context;
        }
        public override Task Add(SubTask entity)
        {
            ValidateData(entity);
            return base.Add(entity);
        }
        public override async Task Delete(int id)
        {
            try
            {
                var subtask = await _context.SubTask.FindAsync(id);
                if (subtask != null)
                {
                    _context.SubTask.Remove(subtask);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("subtask not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public override Task<List<SubTask>> GetAll()
        {
            return base.GetAll();
        }
        public override Task<SubTask?> GetById(int id)
        {
            return base.GetById(id);
        }
        public override Task Update(SubTask entity)
        {
            ValidateData(entity);
            return base.Update(entity);
        }
        public void ValidateData(SubTask sub)
        {
            if (string.IsNullOrWhiteSpace(sub.SubTaskName))
            {
                throw new Exception("subtask must have a name");
            }
        }
    }
}
