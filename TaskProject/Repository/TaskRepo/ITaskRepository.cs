using Task = TaskProject.Models.Task;

namespace TaskProject.Repository.TaskRepo
{
    public interface ITaskRepository : IRepository<Task>
    {
        public  Task<List<Task>> GetAllAsync();

        public Task<Task> GetSpecificAsync(int id);

        public Task<bool> DeleteTask(int id);


        public  Task<bool> InsertTaskAsync(Task task);

        public Task<List<ApplicationUser>> GetUsersOfTask(int taskID);

        public Task<List<Task>> GetTasksByUserId(string userId);



    }
}