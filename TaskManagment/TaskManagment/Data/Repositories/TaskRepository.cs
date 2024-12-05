using TaskManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Interfaces;

namespace TaskManagement.Data.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        private readonly TaskDataContext _context;
        public TaskRepository(TaskDataContext context)
        {
            _context = context;
        }
        public async Task<List<TaskEntity>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }
        public async Task<TaskEntity> GetTaskByTitle(string title)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Title == title);
        }
       
        public async Task<TaskEntity> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<TaskEntity> CreateTask(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<TaskEntity> UpdateTask(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task DeleteTask(int id)
        {
            var task = await GetTaskById(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

    }
}
