using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Helpers;
using TaskProject.ViewModels.Tasks;
using TaskProject.ViewModels.UserVM;

namespace TaskProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult UserHome()
        {
            return View("UserHome");
        }
        public async Task<IActionResult> AllUser()
        {
            List<AppUserVM> appUserVMs = await unitOfWork.UserService.GetAllAppUsers();
            //List<TaskViewModel>  taskViewModels = await unitOfWork.TaskService.GetAllTasks();
            if (appUserVMs == null)
            {
                RedirectToAction("Not_Found ", "Project");
            }
         
            return View("AllUser" ,appUserVMs);
        }

        public async Task<IActionResult> SpecificUser(string id)
        {
            AppUserVM appUserVMs = await unitOfWork.UserService.GetSpecificAppUsers(id);
            List<TaskViewModel> taskViewModels = await unitOfWork.TaskService.GetAllTasks();

            if (appUserVMs == null)
            {
                RedirectToAction("Not_Found ", "Project");
            }
            Task_AppUser_VM task_AppUser_VM = new Task_AppUser_VM()
            {
                taskViewModels = taskViewModels,
                appUserVMs = appUserVMs

            };
            return View("SpecificUser", task_AppUser_VM);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTask(string id, int taskId)
        {
            var user = await unitOfWork.UserService.GetSpecificAppUsers(id);
            var task = await unitOfWork.TaskService.GetSpecificTask(taskId);
            if (user == null || task == null)
            {
                RedirectToAction("Not_Found ", "Project");
            }
            //ViewBag.UserId = user.UserId;
            //ViewBag.Tasks = await _taskService.GetAllTasksAsync();
            bool result = await unitOfWork.UserRepository.AssignUserTask(taskId, id);
            if (result)
            {
                return View("UserHome");
            }
            else
            {
                return View("~/Views/Project/Not_Found.cshtml");
            }
        }
        public async Task<IActionResult> FilterTasksByUserAction(string userId)
        {
            List<TaskViewModel> tasks = await unitOfWork.TaskService.GetTasksByUserId(userId);
            return PartialView("_TaskListPartial", tasks);
        }
        // /user / FilterTasksByUser
        public async Task<IActionResult> FilterTasksByUser()
        {
            List<AppUserVM> appUsers = await unitOfWork.UserService.GetAllAppUsers();
            return View("FilterTasksByUser", appUsers);
        }

    }
}
