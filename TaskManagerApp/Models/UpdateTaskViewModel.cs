namespace TaskManagerApp.Models
{
    public class UpdateTaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskManagerApp.Models.Domain.TaskStatus Status { get; set; }
    }
}
