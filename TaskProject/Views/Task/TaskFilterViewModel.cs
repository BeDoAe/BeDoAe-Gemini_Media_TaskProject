using TaskProject.ViewModels.UserVM;

namespace TaskProject.Views.Task
{
    public class TaskFilterViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public List<UserTaskVM> UserTasks { get; set; }
    }
}
