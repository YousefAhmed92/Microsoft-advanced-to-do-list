using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTasks01.Models
{
    public class SubTask
    {
        public int SubTaskId { get; set; }

        [Required(ErrorMessage = "Sub task is required")]
        public string SubTaskName { get; set; }

        public Tasks Task { get; set; }  // Corrected navigation property name
        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }  // Corrected data type
    }
}
