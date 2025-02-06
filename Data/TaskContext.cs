using Microsoft.EntityFrameworkCore;

using TaskApi.Models;

namespace TaskApi.Data;

public class TaskContext: DbContext {
     public DbSet<TaskModel> Task { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=task.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}