using TaskApi.Data;
using TaskApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskApi.Routes;

public static class TaskRoute {
    public static void TaskRoutes(this WebApplication app)
    {
        var route = app.MapGroup("task");

        route.MapPost("", async (TaskRequest req, TaskContext context ) => 
        {
        var task = new TaskModel(req.taskName, req.category, req.priority, req.date);
        await context.AddAsync(task);
        await context.SaveChangesAsync();
    
        return Results.Created($"/task/{task.Id}", task); 
        });

        route.MapGet("", async (TaskContext context) => 
        {
            var task = await context.Task.ToListAsync();
            return Results.Ok(task);
        });
        <p></p>

        route.MapPut("{id:guid}", async (Guid id, TaskRequest req, TaskContext context) => 
        {
            var task = await context.Task.FirstOrDefaultAsync(x => x.Id == id);

            if (task == null){
                return Results.NotFound();
            }

            task.ChangeTaskName(req.taskName);
            await context.SaveChangesAsync(); 

            task.ChangeCategory(req.category);
            await context.SaveChangesAsync();

            task.ChangePriority(req.priority);
            await context.SaveChangesAsync();

            task.ChangeDate(req.date);
            await context.SaveChangesAsync();

            return Results.Ok(task);
        });

        route.MapDelete("{id:guid}", async (Guid id, TaskContext context) => 
        {
            var task = await context.Task.FirstOrDefaultAsync(x => x.Id == id);

            if (task == null){
                return Results.NotFound();
            }

            await Task.Delay(2000);

            context.Task.Remove(task);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        route.MapDelete("cleanup", async (TaskContext context) =>
        {
            var now = DateTime.UtcNow;

            var expiredTasks = await context.Task
                .Where(t => t.Date < now) 
                .ToListAsync();

            if (expiredTasks.Count == 0)
            {
                return Results.NotFound("Nenhuma tarefa vencida encontrada.");
            }

            context.Task.RemoveRange(expiredTasks);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}