using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskProject.Models
{
    public class Subtask
    {
        public int Id { get; set; }

    
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public bool IsDeleted { get; set; } = false;


        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task? Task { get; set; }
    }
}
