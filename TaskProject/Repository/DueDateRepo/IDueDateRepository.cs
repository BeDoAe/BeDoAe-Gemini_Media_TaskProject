using Task = TaskProject.Models.Task;

namespace TaskProject.Repository.DueDateRepo
{
    public interface IDueDateRepository
    {
        public Task<List<Task>> GetOverdueTasksAsync(int count);

    }
}