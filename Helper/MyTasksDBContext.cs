using Microsoft.EntityFrameworkCore;

namespace MyTasks01.Helper
{
    public class MyTasksDBContext : DbContext
    {
        //public MyTasksDBContext()
        //{
            
        //}
        public MyTasksDBContext(DbContextOptions<MyTasksDBContext> option) : base(option)
        {
            
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Models.Tasks>()
                .HasOne(t => t.Priority)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.PriorityId);

            modelBuilder.Entity<Models.Tasks>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tasks)
                .HasForeignKey(s => s.StatusId);

            //modelBuilder.Entity<Models.Tasks>()
            //    .HasOne(t => t.Sprint)
            //    .WithMany(s => s.Tasks)
            //    .HasForeignKey(s => s.SprintId);

            modelBuilder.Entity<Models.SubTask>()
                .HasOne(s => s.Task)
                .WithMany(s => s.Subtasks)
                .HasForeignKey(s => s.TaskId);

            modelBuilder.Entity<Models.Tasks>()
                .HasOne(t => t.Lists)
                .WithMany(t => t.tasks)
                .HasForeignKey(t => t.ListsId);


        }
        public DbSet<Models.Tasks> Task { get; set; }
        public DbSet<Models.SubTask> SubTask { get; set; }
        public DbSet<Models.Status> Status { get; set; }
        //public DbSet<Models.Sprint> Sprint { get; set; }
        public DbSet<Models.Priority> Priority { get; set; }

        public DbSet<Models.User> User { get; set; }
        public DbSet <Models.Lists> Lists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


    }
}
