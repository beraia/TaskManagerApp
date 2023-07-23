using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Models.Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskStatus Status { get; set; }
    }
}
