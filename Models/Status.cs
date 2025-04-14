using System.ComponentModel.DataAnnotations;

namespace MyTasks01.Models
{
    public class Status
    {
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string StatusName { get; set; }

        public HashSet<Tasks> Tasks { get; set; } = new HashSet<Tasks>();
    }
}
