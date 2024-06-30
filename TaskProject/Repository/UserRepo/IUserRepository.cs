namespace TaskProject.Repository.UserRepo
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        public Task<List<ApplicationUser>> GetAllUsers();

        public  Task<ApplicationUser> GetSpecificUser(string id);

        public Task<bool> AssignUserTask(int taskId, string userId);

    }
}