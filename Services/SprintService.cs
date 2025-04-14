using MyTasks01.Helper;
using MyTasks01.Models;

namespace MyTasks01.Services
{
    //public class SprintService : BaseService<Models.Sprint>
    //{
    //    private readonly MyTasksDBContext _context;

    //    public SprintService(MyTasksDBContext context) : base(context)
    //    {
    //        _context = context;
    //    }
    //    public override Task Add(Sprint entity)
    //    {
    //        ValidateData(entity);
    //        return base.Add(entity);
    //    }
    //    public override Task Delete(int id)
    //    {
    //        return base.Delete(id);
    //    }
    //    public override Task<List<Sprint>> GetAll()
    //    {
    //        return base.GetAll();
    //    }
    //    public override Task<Sprint?> GetById(int id)
    //    {
    //        return base.GetById(id);
    //    }
    //    public override Task Update(Sprint entity)
    //    {
    //        ValidateData(entity);
    //        return base.Update(entity);
    //    }
    //    public void ValidateData(Sprint sprint)
    //    {
    //        if (string.IsNullOrWhiteSpace(sprint.SprintName))
    //            throw new Exception("sprint must have a name");
    //    }

    //}
}
