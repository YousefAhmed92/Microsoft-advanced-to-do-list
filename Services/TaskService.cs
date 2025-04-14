using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyTasks01.Components.Authntication;
using MyTasks01.Components.Tasks;
using MyTasks01.Helper;
using MyTasks01.Models;
using System.Linq.Dynamic.Core;

namespace MyTasks01.Services
{
    public class TaskService : BaseService<Models.Tasks>
    {
        private readonly MyTasksDBContext _context;
        private readonly EmailService _emailService;

        public TaskService(MyTasksDBContext context, EmailService emailService) : base(context)
        {
            _context = context;
            _emailService = emailService;
        }
        public async override Task Add(Models.Tasks entity)
        {
            ValidateData(entity);
            await base.Add(entity);
        }
        public async Task Delete(int id)
        {
            try
            {
                var task = await _context.Task.FindAsync(id);
                if (task != null)
                {
                    _context.Task.Remove(task);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete method: {ex.Message}");
            }
        }
        public override Task<List<Models.Tasks>> GetAll()
        {
            return _context.Task.Include(t => t.Priority).Include(t => t.Lists).Include(t => t.Subtasks).Include(t => t.Status).ToListAsync();
        }
        public async  Task <List<Models.Tasks>> GetAllImportantTasks()
        {
            return  await _context.Task.Where(t => t.PriorityId == 1).Include(t => t.Lists).Include(t=> t.Status).Include(t => t.Priority).ToListAsync();
        }

        public async Task<List<Models.Tasks>> GetCompletedTasksWithinDeadline()
        {
            return await _context.Task.Where(t => t.TaskCompletionStatus == "Submitted Within Deadline").Include(t => t.Status).Include(t => t.Priority).Include(t => t.Lists).ToListAsync();
        }

        public async Task<List<Models.Tasks>> GetInMyDayTasks()
        {
            return await _context.Task.Where(t => t.InMyDay == true ).Include(t => t.Status).Include(t => t.Lists).Include(t => t.Priority).AsNoTracking().ToListAsync();
        }

        public async Task<List<Models.Tasks>> GetCompletedTasksAfterDeadline()
        {
            return await _context.Task.Where(t => t.TaskCompletionStatus == "Submitted After Deadline").Include(t => t.Status).Include(t => t.Lists).Include(t => t.Priority).ToListAsync();
        }

        public async Task AddSubTask(int TaskId, string SubTaskName)
        {
            var NewSubTask = new Models.SubTask
            {
                TaskId = TaskId,
                SubTaskName = SubTaskName
            };
            _context.SubTask.Add(NewSubTask);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Models.Tasks>> GetTasksInList(int id)
            => await _context.Task.Include(t => t.Priority).Include(t => t.Lists).Include(t => t.Subtasks).Where(t => t.ListsId == id).ToListAsync();

        public async Task<Models.Tasks?> GetById(int id)
        {
            return await _context.Task.Include(t => t.Priority).Include(t => t.Lists).Include(t => t.Subtasks).Include(t => t.Status).FirstOrDefaultAsync(t => t.TaskId == id); ;
        }
        public override Task Update(Models.Tasks entity)
        {
            ValidateData(entity);
            return base.Update(entity);
        }
        public async Task UpdateTaskStatus(Models.Tasks entity)
        {
              await base.Update(entity);
        }
        public async Task UpdateMyDayTasksStatus(Models.Tasks tasks)
        {
            await base.Update(tasks);
        }

        public async Task ToggleTaskAsImportant(Models.Tasks task)
        {
            await base.Update(task);
        }
        public void ValidateData(Models.Tasks task)
        {
            if (string.IsNullOrWhiteSpace(task.TaskName))
                throw new InvalidDataException("task name is required");
            if (task.NotifierInHours < 0)
                throw new InvalidDataException("reminder time must be with minutes");
        }

        public async Task SendTaskNotifierEmail(Models.Tasks task)
        {
            if (task == null) throw new ArgumentNullException("this task does not exist");
            if ((task.NotifierWhen - DateTime.Now).Value.Minutes == 1)
            {
                var email = "yousefahmed462003@gmail.com";
                var subject = "Task Reminder Fot You";
                var body = $"i would Like to remeber you that task ${task.TaskName} ";
                try
                {
                    Console.WriteLine($"Sending email for task: {task.TaskName} at {DateTime.Now}");
                    await _emailService.SendEmailService(email, subject, body);
                    Console.WriteLine("Email sent successfully.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
        }

        public async Task<string> ReminderMessage(Models.Tasks task)
        {
            if (task.NotifierInHours == null)
            {
                Console.WriteLine($"No reminder set for task: {task.TaskName}");
                return "No reminder set for this task.";
            }

            TimeSpan? timeLeft = task.Deadline - DateTime.Now;
            Console.WriteLine($"Task: {task.TaskName}, Minutes left: {timeLeft?.TotalMinutes}");

            // Default values for subject and body
            string subject = "";
            string body = "";

            if (task.NotifierWhen.HasValue)
            {
                TimeSpan timeRight = task.NotifierWhen.Value - DateTime.Now;

                // Check if the time difference is less than 1 minute
                if (Math.Abs(timeRight.TotalMinutes) < 1)
                {
                    Console.WriteLine($"Reminder triggered for task: {task.TaskName}");

                    // Create email content
                    subject = $"Task Reminder: {task.TaskName}";
                    body = $@"
                <html>
                <body>
                    <h2>Task Reminder</h2>
                    <p>Your task <strong>{task.TaskName}</strong> is due soon!</p>
                    <p>Deadline: {task.Deadline}</p>
                    <p>Please complete it as soon as possible.</p>
                </body>
                </html>";

                    // Send the email
                    await _emailService.SendEmailService("yousefahmed462003@gmail.com", subject, body);

                    return $"Reminder for task: {task.TaskName}. is now!";
                }
            }

            return "No reminder due at this time.";
        }

        public async Task<List<Models.Tasks>> GetTasksToRemind()
        {
            var tasks = await _context.Task
                .Where(t => t.NotifierInHours != null && t.IsCompletedWithinSDeadline == false)
                .ToListAsync();

            Console.WriteLine($"Total tasks fetched: {tasks.Count}");

            var taskstoremind = new List<Models.Tasks>();
            foreach (var task in tasks)
            {
                TimeSpan? timeLeft = task.Deadline - DateTime.Now;
                Console.WriteLine($"Task: {task.TaskName}, Minutes Left: {timeLeft?.TotalMinutes}");

                if (task.NotifierWhen == DateTime.Now)
                {
                    taskstoremind.Add(task);
                    Console.WriteLine($"Task added to reminders: {task.TaskName}");
                }
            }
            return taskstoremind;
        }

    }
}
