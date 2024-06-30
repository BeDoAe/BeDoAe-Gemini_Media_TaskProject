using System.ComponentModel.DataAnnotations;
using TaskProject.ViewModels.SubTask;
using TaskProject.ViewModels.UserVM;

namespace TaskProject.ViewModels.Tasks
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TaskDueDate { get; set; }

        public string? TaskAttachment { get; set; } // This will store the file path

        public bool TaskIsDeleted { get; set; }=false;

        public int ProjectId { get; set; }
        public List<SubTaskViewModel> TaskSubtasks { get; set; } = new List<SubTaskViewModel>();

       public List<AppUserVM>? TaskAppUsers { get; set; }
    }
}
