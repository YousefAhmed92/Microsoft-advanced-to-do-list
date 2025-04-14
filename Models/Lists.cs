namespace MyTasks01.Models
{
    public class Lists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<Models.Tasks>? tasks { get; set; } = new HashSet<Models.Tasks>();

    }
}
