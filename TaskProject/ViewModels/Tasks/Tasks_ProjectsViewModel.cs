using Task = TaskProject.Models.Task;
using project = TaskProject.Models.Project;
using TaskProject.ViewModels.Project;
namespace TaskProject.ViewModels.Tasks
{
    public class Tasks_ProjectsViewModel
    {
    

        public TaskViewModel Task { get; set; }
        public List<TaskProjectViewModel>? Projects { get; set; }

    }
}
