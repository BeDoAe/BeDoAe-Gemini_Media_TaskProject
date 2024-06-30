using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskProject.Helpers;
using TaskProject.Models;
using TaskProject.ViewModels.Project;
using TaskProject.ViewModels.Tasks;


namespace TaskProject.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

       public TaskController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // Get All Tasks           /Task/AllTasks
        public async Task<IActionResult> AllTasks()
        {
            List<TaskViewModel> tasks = await unitOfWork.TaskService.GetAllTasks();
            return View("AllTasks", tasks);
        }

        // Get Specific Task
        public async Task<IActionResult> GetSpecific(int id)
        {
            var task = await unitOfWork.TaskService.GetSpecificTask(id);
            if (task == null)
            {
                //return NotFound();
                return View("~/Views/Project/Not_Found.cshtml");
            }
            return View("GetSpecificTask", task);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var task = await unitOfWork.TaskService.GetSpecificTask(id);
            if (task == null)
            {
                return View("~/Views/Project/Not_Found.cshtml");
            }

            var projects = await unitOfWork.ProjectService.GetAllProjects();
            var viewModel = new Tasks_ProjectsViewModel
            {
                Task = new TaskViewModel
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    TaskDescription = task.TaskDescription,
                    TaskDueDate = task.TaskDueDate,
                    TaskAttachment = task.TaskAttachment // Existing attachment path
                },
                Projects = projects.Select(p => new TaskProjectViewModel
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName
                }).ToList()
            };

            return View("UpdateTask", viewModel);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Update(Tasks_ProjectsViewModel viewModel, IFormFile newAttachment)
        {
            if (ModelState.IsValid)
            {
                var updatedTask = await unitOfWork.TaskService.GetSpecificTask(viewModel.Task.TaskId);
                if (updatedTask == null)
                {
                    return NotFound();
                }

                // Update task properties
                updatedTask.ProjectId = viewModel.Task.ProjectId;
                updatedTask.TaskName = viewModel.Task.TaskName;
                updatedTask.TaskDescription = viewModel.Task.TaskDescription;
                updatedTask.TaskDueDate = viewModel.Task.TaskDueDate;

                if (newAttachment != null && newAttachment.Length > 0)
                {
                    updatedTask.TaskAttachment = await FileHelper.SaveFileAsync(newAttachment);
                }

                await unitOfWork.TaskService.UpdateTask(updatedTask);
                unitOfWork.Save();

                return RedirectToAction(nameof(AllTasks));
            }

            var projects = await unitOfWork.ProjectService.GetAllProjects();
            viewModel.Projects = projects.Select(p => new TaskProjectViewModel
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName
            }).ToList();

            return View("UpdateTask", viewModel);
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var task = await unitOfWork.TaskService.GetSpecificTask(id);
            if (task == null)
            {
                return View("~/Views/Project/Not_Found.cshtml");
            }
            return View("DeleteTask", task);

        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int taskId)
        {
            var success = await unitOfWork.TaskRepository.DeleteTask(taskId);
            if (success)
            {
                unitOfWork.Save();
                return RedirectToAction(nameof(AllTasks));
            }
            return View("~/Views/Project/Not_Found.cshtml");
        }

        public async Task<IActionResult> Assign(int id)
        {
            var task = await unitOfWork.TaskService.GetSpecificTask(id);
            var projects = await unitOfWork.ProjectService.GetAllProjects();
            if (task == null || projects == null)
            {
                return View("~/Views/Project/Not_Found.cshtml");
            }
            Tasks_ProjectsViewModel taskViewModel = new Tasks_ProjectsViewModel
            {
                Task = task,
                Projects = projects.Select(p => new TaskProjectViewModel
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName
                }).ToList()
            };
            return View("AssignTask", taskViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Assign(TaskViewModel taskViewModel, int projectId)
        {
            if (ModelState.IsValid)
            {
                var success = await unitOfWork.TaskService.AssignTask(taskViewModel, projectId);
                if (success)
                {
                    unitOfWork.Save();

                    return RedirectToAction(nameof(AllTasks));
                }
            }
            return View("AssignTask", taskViewModel);
        }

        // Insert Task  /Task/Insert
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Insert()
        {
            var projects = await unitOfWork.ProjectService.GetAllProjects();
            var viewModel = new Tasks_Project_FormFile_ViewModel
            {
                TaskFormFile = new TaskFormFileViewModel(),
                Projects = projects.Select(p => new TaskProjectViewModel
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName
                }).ToList()
            };

            return View("InsertTask", viewModel);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Insert(Tasks_Project_FormFile_ViewModel  tasks_Project_FormFile_ViewModel)
        {
            if (ModelState.IsValid)
            {
               await unitOfWork.TaskService.InsertTask(tasks_Project_FormFile_ViewModel.TaskFormFile);
                unitOfWork.Save();

                return RedirectToAction(nameof(AllTasks));
            }

            // Reload the projects list in case of validation failure
            var projects = unitOfWork.ProjectService.GetAllProjects().Result;
            tasks_Project_FormFile_ViewModel.Projects = projects.Select(p => new TaskProjectViewModel
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName
            }).ToList();

            return View("InsertTask", tasks_Project_FormFile_ViewModel);
        }

        

    }
}
