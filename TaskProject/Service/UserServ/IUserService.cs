using TaskProject.ViewModels.UserVM;

namespace TaskProject.Service.UserServ
{
    public interface IUserService : IService<ApplicationUser>
    {
        public Task<List<AppUserVM>> GetAllAppUsers();

        public Task<AppUserVM> GetSpecificAppUsers(string id);


    }
}