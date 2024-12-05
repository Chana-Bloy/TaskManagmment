using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TaskManagement.Data
{
    public class TaskDataContext : DbContext
    {
        public TaskDataContext(DbContextOptions<TaskDataContext> options) : base(options)
        {
        }
        public DbSet<Entities.TaskEntity> Tasks { get; set; }
    }
}
