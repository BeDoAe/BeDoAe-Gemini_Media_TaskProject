using TaskProject.ViewModels.Tasks;
using Task = TaskProject.Models.Task;

namespace TaskProject.Service.TaskServ
{
    public interface ITaskService :IService<Task> 
    {
        //public void InsertTask(TaskViewModel taskViewModel);
        public Task<bool> InsertTask(TaskFormFileViewModel taskFormFileView);

        public Task<TaskViewModel> GetSpecificTask(int id);

        public  Task<List<TaskViewModel>> GetAllTasks();

        public Task<Task> UpdateTask(TaskViewModel taskViewModel);

        public Task<bool> AssignTask(TaskViewModel taskViewModel, int ProjectId);

        public Task<List<TaskViewModel>> GetTasksByUserId(string userId);

    }
}