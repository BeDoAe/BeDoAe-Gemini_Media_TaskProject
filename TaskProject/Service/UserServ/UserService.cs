using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskProject.Repository.TaskRepo;
using TaskProject.Repository.UserRepo;
using TaskProject.Service.TaskServ;
using TaskProject.ViewModels.Tasks;
using TaskProject.ViewModels.UserVM;
using Task = TaskProject.Models.Task;


namespace TaskProject.Service.UserServ
{
    public class UserService : IService<ApplicationUser>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ITaskService taskService;

        public UserService(IUserRepository _userRepository , ITaskService _taskService)
        {
            userRepository = _userRepository;
            taskService = _taskService;
        }


        //        public async Task<List<AppUserVM>> GetAllAppUsers()
        //        {
        //            List<ApplicationUser> appUsers = await userRepository.GetAllUsers();

        //            List<AppUserVM> appUserVMs = appUsers.Select(x => new AppUserVM
        //            {
        //                idVM = x.Id,
        //                FirstNameVM = x.FirstName,
        //                LastNameVM = x.LastName,
        //                UsernameVM=x.UserName,
        //                EmailVM =x.Email,
        //                UserTasksVM = x.UserTasks.Select(ut => new UserTaskVM
        //                {
        //                    IdVM = ut.Id,
        //                    IsDeletedVM = ut.IsDeleted,
        //                    TaskIdVM = ut.TaskId,
        //                    UserIdVM = ut.UserId,


        //                }).ToList()
        //            }).ToList();

        //            return appUserVMs;
        //        }

        //        public async Task<AppUserVM> GetSpecificAppUsers(string id)
        //        {
        //            ApplicationUser appUser = await userRepository.GetSpecificUser(id);

        //            if (appUser == null)
        //            {
        //                return null;
        //            }

        //            AppUserVM appUserVMs = new AppUserVM
        //            {
        //                idVM = appUser.Id,
        //                FirstNameVM = appUser.FirstName,
        //                LastNameVM = appUser.LastName,
        //                UsernameVM=appUser.UserName,
        //                EmailVM =appUser.Email,
        //                UserTasksVM = appUser.UserTasks?.Select(ut => new UserTaskVM
        //                {
        //                    IdVM = ut.Id,
        //                    IsDeletedVM = ut.IsDeleted,
        //                    TaskIdVM = ut.TaskId,
        //                    UserIdVM = ut.UserId,
        //                }).ToList()
        //            };

        //            return appUserVMs;
        //        }
        //    }
        //}


        public async Task<List<AppUserVM>> GetAllAppUsers()
        {
            List<ApplicationUser> appUsers = await userRepository.GetAllUsers();
            List<AppUserVM> appUserVMs = new List<AppUserVM>();

            foreach (var user in appUsers)
            {
                var userTasks = new List<UserTaskVM>();
                foreach (var userTask in user.UserTasks)
                {
                    var task = await taskService.GetSpecificTask(userTask.TaskId);
                    userTasks.Add(new UserTaskVM
                    {
                        IdVM = userTask.Id,
                        IsDeletedVM = userTask.IsDeleted,
                        TaskIdVM = userTask.TaskId,
                        TaskNameVM = task.TaskName,  // Assign the task name
                        UserIdVM = userTask.UserId
                    });
                }

                appUserVMs.Add(new AppUserVM
                {
                    idVM = user.Id,
                    UsernameVM = user.UserName,  // Include UsernameVM
                    FirstNameVM = user.FirstName,
                    LastNameVM = user.LastName,
                    EmailVM = user.Email,
                    UserTasksVM = userTasks
                });
            }

            return appUserVMs;
        }

        public async Task<AppUserVM> GetSpecificAppUsers(string id)
        {
            ApplicationUser appUser = await userRepository.GetSpecificUser(id);

            if (appUser == null)
            {
                return null;
            }

            var userTasks = new List<UserTaskVM>();
            foreach (var userTask in appUser.UserTasks)
            {
                var task = await taskService.GetSpecificTask(userTask.TaskId);
                userTasks.Add(new UserTaskVM
                {
                    IdVM = userTask.Id,
                    IsDeletedVM = userTask.IsDeleted,
                    TaskIdVM = userTask.TaskId,
                    TaskNameVM = task.TaskName,  // Assign the task name
                    UserIdVM = userTask.UserId
                });
            }

            AppUserVM appUserVMs = new AppUserVM
            {
                idVM = appUser.Id,
                UsernameVM = appUser.UserName,  // Add this line
                FirstNameVM = appUser.FirstName,
                LastNameVM = appUser.LastName,
                EmailVM = appUser.Email,
                UserTasksVM = userTasks
            };

            return appUserVMs;
        }

      
    }
}