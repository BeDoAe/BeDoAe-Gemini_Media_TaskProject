using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskProject.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public string? Attachment { get; set; } // This will store the file path

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public List<Subtask>? Subtasks { get; set; }

        public List<UserTasks>? UserTasks { get; set; }
    }
}
