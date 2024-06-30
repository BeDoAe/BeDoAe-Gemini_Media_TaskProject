using System.ComponentModel.DataAnnotations;

namespace TaskProject.ViewModels.SubTask
{
    public class SubTaskViewModel
    {
        public int SubTaskId { get; set; }


        public string SubTaskName { get; set; }

        public string SubTaskDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SubTaskDueDate { get; set; }

        public bool SubtaskIsDeleted { get; set; } = false;


    }
}
