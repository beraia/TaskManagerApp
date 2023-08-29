using MediatR;
using TaskManagerApp.Data;

namespace TaskManagerApp.Commands
{
    public class DeleteTaskCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Guid>
    {
        private readonly ApplicationDbContext _context;
        public DeleteTaskHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(command.Id);

            if (task != null)
            {
                _context.Tasks.Remove(task);

                await _context.SaveChangesAsync();
            }

            return command.Id;
        }
    }
}
