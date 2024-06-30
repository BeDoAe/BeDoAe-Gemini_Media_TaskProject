using Microsoft.EntityFrameworkCore;
using Task = TaskProject.Models.Task;

namespace TaskProject.Repository.DueDateRepo
{
    public class DueDateRepository: IDueDateRepository
    {
        private readonly Context context;

        public DueDateRepository(Context context)
        {
            this.context = context;
        }
        public async Task<List<Task>> GetOverdueTasksAsync(int count)
        {
            List<Task> tasks = 
             await context.Tasks
                .Where(t => t.IsDeleted == false && t.DueDate < DateTime.Now)
                .OrderByDescending(t => t.DueDate)
                .Take(count)
                .ToListAsync();

            return tasks;
        }
    }
}
