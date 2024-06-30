using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Helpers;
using TaskProject.Service.Project;
using TaskProject.ViewModels.Project;
using TaskProject.ViewModels.SubTask;
using TaskProject.ViewModels.Tasks;

namespace TaskProject.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProjectController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await unitOfWork.ProjectService.GetAllProjects();
            return View("Index",projects);
        }
        // /Project/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
      {
            return View("CreateProject");
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Create(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                 unitOfWork.ProjectService.InsertProject(projectViewModel);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View("Not_Found");
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            var project = await unitOfWork.ProjectService.GetSpecificProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return View("Edit_Project", project);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.ProjectService.UpdateProject(projectViewModel);
                 unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View("Edit_Project", projectViewModel);
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var project = await unitOfWork.ProjectService.GetSpecificProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return View("Delete_Project", project);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int ProjectId)
        {
            bool result = await unitOfWork.ProjectRepository.DeleteProject(ProjectId);
            if (!result)
            {
                return View("Not_Found");
            }
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}