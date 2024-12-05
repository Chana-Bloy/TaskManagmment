using TaskManagement.Data.Entities;
using TaskManagement.Data.Models;
using TaskManagement.Data.Repositories;
using TaskManagement.Helpers;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using TaskManagement.Interfaces;

namespace TaskManagement.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<TaskEntity>> GetAll()
        {
            List<TaskEntity> existTask = await _taskRepository.GetAllTasks();
            
            return existTask;
        }
        public async Task<TaskEntity> GetById(int id)
        {
            TaskEntity existTask = await _taskRepository.GetTaskById(id);
            if (existTask == null)
            {
                throw new NotFoundException("task doesn't exist");
            }
            return existTask;
        }

        public async Task<TaskEntity> AddNew(CreateTaskRequest task)
        {
            TaskEntity existTask = await _taskRepository.GetTaskByTitle(task.Title);
            if (existTask != null)
            {
                throw new AppException("title must be uniq");
            }
            if (string.IsNullOrEmpty(task.Title))
            {
                throw new AppException("title is required");
            }

            TaskEntity t = new TaskEntity()
            {
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };
            TaskEntity newTask = await _taskRepository.CreateTask(t);

            return newTask;
        }
        public async Task<TaskEntity> Update(int id, UpdateTaskRequest updateRequest)
        {
            TaskEntity existTask = await _taskRepository.GetTaskById(id);
            if (existTask == null)
            {
                throw new NotFoundException("task doesn't exist");
            }
           
            TaskEntity existTaskTitle = await _taskRepository.GetTaskByTitle(updateRequest.Title);
            if (existTaskTitle != null && existTaskTitle.Id != id)
            {
                throw new AppException("task title must be uniq");
            }

            existTask.Title = updateRequest.Title;
            existTask.Description = updateRequest.Description;
            existTask.IsCompleted = updateRequest.IsCompleted;

            TaskEntity updetedTask = await _taskRepository.UpdateTask(existTask);

            return updetedTask;

        }
        public async Task Delete(int id)
        {
            TaskEntity existTask = await _taskRepository.GetTaskById(id);
            if (existTask == null)
            {
                throw new NotFoundException("task doesn't exist");
            }
           await _taskRepository.DeleteTask(id);
        }

    }
}
