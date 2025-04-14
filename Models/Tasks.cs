using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTasks01.Models
{
    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        //[Required(ErrorMessage = "Task name is required")]
        public string TaskName { get; set; }
        public  string?  TaskDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        //public Sprint? Sprint { get; set; }
        //[ForeignKey(nameof(Sprint))]
        public int? SprintId { get; set; }
        public Status? Status { get; set; }
        [ForeignKey(nameof(Status))]
        public int? StatusId { get; set; }
        public Priority? Priority { get; set; }  // Corrected spelling
        [ForeignKey(nameof(Priority))]
        public int? PriorityId { get; set; }  // Corrected spelling
        public bool? IsCompletedWithinSDeadline { get; set; }
        public bool? IsCompletedAfterSDeadline { get; set; }
        public DateTime? LastUpdated { get; set; }
        public HashSet<SubTask>? Subtasks { get; set; } = new HashSet<SubTask>();
        public DateTime? Deadline { get; set; }
        public int? NotifierInHours  { get; set; }

        public DateTime? NotifierWhen { get; set; }



        [EmailAddress]
        public string? Email { get; set; }
        public string TaskCompletionStatus { get; set; } = "Working On";
        public bool InMyDay { get; set; }

        public Lists? Lists { get; set; }
        [ForeignKey("Lists")]
        public int? ListsId { get; set; }

        public PlannedTasks? PlannedTasks { get; set; }
        [ForeignKey("PlannedTasks")]
        public int? PlannedTaskId { get; set; }


    }
}
