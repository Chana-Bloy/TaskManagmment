using TaskManagement.Data;
using TaskManagement.Data.Repositories;
using TaskManagement.Helpers;
using TaskManagement.Services;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
          builder =>
          {
              builder.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
          });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<TaskDataContext>(options => options.UseInMemoryDatabase("TaskDb"));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<DataHelper>();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TaskManagementApi", Version = "v1" });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dataHelper = services.GetRequiredService<DataHelper>();
        dataHelper.SeedData(); // קריאה לפונקציה
    }
    catch (Exception ex)
    {
        // טיפול בחריגות (לדוגמה: רישום ליומן)
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Managment Api v1"));
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors("AllowAllHeaders");
app.MapControllers();

app.Run();

public partial class Program { }