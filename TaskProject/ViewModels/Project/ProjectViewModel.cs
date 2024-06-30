using System.ComponentModel.DataAnnotations;
using TaskProject.Models;
using TaskProject.ViewModels.Tasks;
using Task = TaskProject.Models.Task;
namespace TaskProject.ViewModels.Project
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        public List<TaskViewModel> ProjectTasks { get; set; } = new List<TaskViewModel>();
    }
}
