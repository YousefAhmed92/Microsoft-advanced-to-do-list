using System.ComponentModel.DataAnnotations;

namespace MyTasks01.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }  
        public string PriorityName { get; set; }  
        public HashSet<Tasks> Tasks { get; set; } = new HashSet<Tasks>();
    }
}
