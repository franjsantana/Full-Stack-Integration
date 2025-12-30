var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory list to store tasks
var tasks = new List<TaskItem>();

// GET all tasks
app.MapGet("/tasks", () => Results.Ok(tasks));

// POST - Add a new task
app.MapPost("/tasks", (TaskItem task) =>
{
    tasks.Add(task);
    return Results.Created($"/tasks/{task.Id}", task);
});

// PUT - Update a task
app.MapPut("/tasks/{id}", (int id, TaskItem updatedTask) =>
{
    var existingTask = tasks.FirstOrDefault(t => t.Id == id);// Find the task by ID
    if (existingTask == null)
        return Results.NotFound();

    existingTask.Name = updatedTask.Name;// Update the task name
    existingTask.IsCompleted = updatedTask.IsCompleted;// Update the task completion status
    return Results.Ok(existingTask);
});

// DELETE - Remove a task
app.MapDelete("/tasks/{id}", (int id) =>
{
    var existingTask = tasks.FirstOrDefault(t => t.Id == id);
    if (existingTask == null)
        return Results.NotFound();

    tasks.Remove(existingTask);
    return Results.NoContent();
});

app.Run();



















/*
Create a simple in-memory task list to store data.
Define a GET route to retrieve all tasks.
*/
