using System.ComponentModel;

namespace MyTasks01.Models
{
    public class PlannedTasks
    {
        public int PlannedTasksId { get; set; }
        public HashSet<Tasks> Tasks { get; set; } = new HashSet<Tasks>();
        public DateTime RepeatUntil { get; set; }
        public PlannedCategories PlannedCategories { get; set; }
    }
    public enum PlannedCategories
    {
        [Description("null")]
        None = 0,

        [Description("daily")]
        Daily = 1,

        [Description("Weekly")]
        Weekly = 2,

        [Description("Monthly")]
        Monthly = 3,

        [Description("Specific Date")]
        SpecificDate = 4,
    }
}
