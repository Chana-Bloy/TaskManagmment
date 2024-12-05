using TaskManagement.Data;
using TaskManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Helpers
{
    public class DataHelper
    {
        private readonly TaskDataContext _taskDataContext;
        public DataHelper(TaskDataContext taskDataContext)
        {
            _taskDataContext = taskDataContext;
        }
        public void SeedData()
        {
            if (_taskDataContext.Tasks.Any())
                return;
            var tasks = new List<TaskEntity>();
            for (var i = 1; i < 50; i++)
                tasks.Add(new TaskEntity { Id = i, Title = $"Task {i+1}", Description = $"Description for Task {i+1}", IsCompleted = false });
            _taskDataContext.Tasks.AddRange(tasks);
            _taskDataContext.SaveChanges();
        }
        
    }
}
