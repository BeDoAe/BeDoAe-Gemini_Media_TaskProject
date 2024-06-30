using System.ComponentModel.DataAnnotations;
using TaskProject.ViewModels.SubTask;

namespace TaskProject.ViewModels.Tasks
{
    public class TaskFormFileViewModel
    {
       


        public int FormFile_TaskId { get; set; }

        public string FormFile_TaskName { get; set; }

        public string FormFile_TaskDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FormFile_TaskDueDate { get; set; }

        public IFormFile? FormFile_TaskAttachment { get; set; } // This will store the file path


        public int FormFile_ProjectId { get; set; }
        public List<SubTaskViewModel>? FormFile_TaskSubtasks { get; set; }
    }
}
