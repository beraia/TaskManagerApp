using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskManagerApp.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManagerApp.Commands;

public class CreateTaskCommand : IRequest<CommandExecutionResult>
{
    public string Title { get; set; }
    public string Content { get; set; }

}

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, CommandExecutionResult>
{
    private readonly ApplicationDbContext _context;
    public CreateTaskHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CommandExecutionResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskManagerApp.Models.Domain.Task()
        {
            Title = request.Title,
            Content = request.Content
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        
        return new CommandExecutionResult();
    }
}

public class CommandExecutionResult
{
    public Guid Id { get; set; }
}