using TaskManagement.Data.Entities;

namespace TaskManagement.Interfaces
{
    public interface ITaskRepository
    {
        public  Task<List<TaskEntity>> GetAllTasks();
        public  Task<TaskEntity> GetTaskByTitle(string title);
        public  Task<TaskEntity> GetTaskById(int id);
        public  Task<TaskEntity> CreateTask(TaskEntity task);
        public  Task<TaskEntity> UpdateTask(TaskEntity task);
        public Task DeleteTask(int id);
    }
}
