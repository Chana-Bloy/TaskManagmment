using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.Models
{
    public class CreateTaskRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

    }
}
