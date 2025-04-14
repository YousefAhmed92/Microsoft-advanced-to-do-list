namespace MyTasks01.Services
{
    public class NotifierBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _service;

        public NotifierBackgroundService(IServiceProvider service)
        {
            _service = service;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("NotifierBackgroundService running");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Checking for tasks due for reminders");

                using (var scope = _service.CreateScope())
                {
                    var taskService = scope.ServiceProvider.GetRequiredService<TaskService>();

                    var taskstoremind = await taskService.GetTasksToRemind();
                    Console.WriteLine($"there is {taskstoremind.Count} tasks due for reminders.");

                    foreach (var task in taskstoremind)
                    {
                        string reminderMessage = await taskService.ReminderMessage(task);
                        Console.WriteLine($" Reminder: {reminderMessage}");
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
