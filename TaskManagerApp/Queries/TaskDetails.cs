using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Queries;

public class GetTaskByIdHandler : IRequestHandler<TaskDetails, TaskManagerApp.Models.Domain.Task>
{
    private readonly ApplicationDbContext _context;
    public GetTaskByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Models.Domain.Task> Handle(TaskDetails query, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == query.Id);

        return task;
    }
}
public class TaskDetails : IRequest<TaskManagerApp.Models.Domain.Task>
{
    public TaskDetails(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
