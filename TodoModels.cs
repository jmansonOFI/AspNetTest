using Microsoft.EntityFrameworkCore;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public string UserId { get; set; }
}
