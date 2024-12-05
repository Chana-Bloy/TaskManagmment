using TaskManagement.Data.Entities;
using TaskManagement.Data.Models;

namespace TaskManagement.Interfaces
{
    public interface ITaskService
    {
        public  Task<List<TaskEntity>> GetAll();
        public  Task<TaskEntity> GetById(int id);
        public Task<TaskEntity> AddNew(CreateTaskRequest task);
        public Task<TaskEntity> Update(int id, UpdateTaskRequest updateRequest);
        public Task Delete(int id);
    }
}
