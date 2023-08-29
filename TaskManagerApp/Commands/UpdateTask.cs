using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;

namespace TaskManagerApp.Commands
{
    public class UpdateTaskCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskStatus Status { get; set; }

        public UpdateTaskCommand(Guid id, string title, string content, TaskStatus status)
        {
            Id = id;
            Title = title;
            Content = content;
            Status = status;
        }
    }

    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Guid>
    {
        private readonly ApplicationDbContext _context;
        public UpdateTaskHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (task != null)
            {
                task.Id = command.Id;
                task.Title = command.Title;
                task.Content = command.Content;
                task.Status = (Models.Domain.TaskStatus)command.Status;

                await _context.SaveChangesAsync();
            }
            return command.Id;
        }
    }
}
