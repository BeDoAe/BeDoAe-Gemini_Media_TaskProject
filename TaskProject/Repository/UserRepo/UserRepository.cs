using Microsoft.EntityFrameworkCore;
using TaskProject.Repository.TaskRepo;

namespace TaskProject.Repository.UserRepo
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly Context context;

        public UserRepository(Context _context) : base(_context)
        {
            this.context = _context;
        }
        public async Task<List<ApplicationUser>>  GetAllUsers()
        {
            List<ApplicationUser> users = 
             await    context.Users
                .Include(u => u.UserTasks)
                .ToListAsync();
            return users;
        }

        public async Task<ApplicationUser> GetSpecificUser(string id)
        {
           ApplicationUser user =
              await  context.Users
                .Include(u=>u.UserTasks)
               .FirstOrDefaultAsync(u=>u.Id==id);
            return user;
        }

        public async Task<bool> AssignUserTask(int taskId, string userId)
        {
            UserTasks userTasks = new UserTasks()
            {
                TaskId = taskId,
                UserId = userId
            };

            if (userTasks != null)
            {
                await context.UserTasks.AddAsync(userTasks);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
