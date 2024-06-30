using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskProject.Models;
using Project = TaskProject.Models.Project;
using Task = TaskProject.Models.Task;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;


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
            return await DeleteProjectWithStoredProcedure(id);
            // Old Query before using Storred Procedure !!

            //var project =
            //  await context.Projects
            //    .Include(p => p.Tasks)
            //    .ThenInclude(t => t.Subtasks)
            //    .FirstOrDefaultAsync(p=>p.Id==id);
            //if (project != null)
            //{
            //    project.IsDeleted = true;
            //    if (project.Tasks != null)
            //    {
            //        foreach (var task in project.Tasks)
            //        {
            //            task.IsDeleted = true;
            //            //Delete userTask as well many-many table
            //            List<UserTasks> usertasks = await context.UserTasks
            //                  .Where(ut => ut.TaskId == task.Id)
            //                  .ToListAsync();
            //            foreach (var ut in usertasks)
            //            {
            //                ut.IsDeleted = true;
            //            }
            //            if (task.Subtasks != null)
            //            {
            //                foreach (var subtask in task.Subtasks)
            //                {
            //                    subtask.IsDeleted = true;
            //                }
            //            }
            //        }
            //    }
            //    await context.SaveChangesAsync(); // Save changes
            //    return true;
            //}
            //return false;
        }
        public async Task<bool> DeleteProjectWithStoredProcedure(int projectId)
        {
          
                // Parameters for the stored procedure
                var projectIdParam = new SqlParameter("@ProjectId", projectId);

                // Execute stored procedure
                int affectedRows = await context.Database.ExecuteSqlRawAsync(
                    "EXEC SoftDeleteProject @ProjectId",
                    projectIdParam);

                return affectedRows > 0; // Return true if any rows were affected
          }
            
    }
}