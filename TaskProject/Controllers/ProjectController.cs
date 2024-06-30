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
        public IActionResult Create()
      {
            return View("CreateProject");
        }

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
        public async Task<IActionResult> Edit(int id)
        {
            var project = await unitOfWork.ProjectService.GetSpecificProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return View("Edit_Project", project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                // Assuming your unitOfWork.ProjectService.UpdateProject method updates the project details
                await unitOfWork.ProjectService.UpdateProject(projectViewModel);
                 unitOfWork.Save(); // Assuming SaveAsync() is an asynchronous save method

                return RedirectToAction("Index");
            }

            // If ModelState is not valid, return to the Edit_Project view with the projectViewModel to display validation errors
            return View("Edit_Project", projectViewModel);
        }


       
        public async Task<IActionResult> Delete(int id)
        {
            var project = await unitOfWork.ProjectService.GetSpecificProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return View("Delete_Project", project);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int ProjectId)
        {
          bool result=  await unitOfWork.ProjectRepository.DeleteProject(ProjectId); // Ensure async deletion
             unitOfWork.Save(); // Ensure async save
            return RedirectToAction(nameof(Index));
        }
    }
}