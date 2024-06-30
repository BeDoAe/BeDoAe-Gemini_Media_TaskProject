using System.ComponentModel.DataAnnotations.Schema;

namespace TaskProject.Models
{
    public class UserTasks
    {
        public int Id { get; set; } // Primary key

        public bool IsDeleted { get; set; } = false;


        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task? Task { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
