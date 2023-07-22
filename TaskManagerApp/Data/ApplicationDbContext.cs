using Microsoft.EntityFrameworkCore;

namespace TaskManagerApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TaskManagerApp.Models.Domain.Task> Tasks { get; set; }
}
