using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskProject.Models;
using Task = TaskProject.Models.Task;

namespace TaskProject.Repository.ProjectRepo
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly Context context;

        public ProjectRepository(Context _context) : base(_context)
        {
            this.context = _context;
        }
        public async Task<List<Project>> GetAllAsync()
        {
            return await context.Projects
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Subtasks)
                .ToListAsync();
        }

        public async Task<Project> GetSpecificAsync(int id)
        {
            return await context.Projects
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Subtasks)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        //public  async void DeleteProject(int id)
        //{
        //    Project p = await GetSpecificAsync(id);
        //    if(p != null) 
        //    {
        //        p.IsDeleted = true;
        //        context.Projects.Remove(p);
        //    }
        //}
        public async Task<bool> DeleteProject(int id)
        {
            Project p = await GetSpecificAsync(id);
            if (p != null)
            {
                p.IsDeleted = true;
                context.Projects.Remove(p);
                await context.SaveChangesAsync(); // Ensure changes are saved asynchronously
                return true;
            }
            return false;
        }



    }
}
