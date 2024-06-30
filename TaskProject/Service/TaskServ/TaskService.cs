using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using TaskProject.Helpers;
using TaskProject.Models;
using TaskProject.Repository.ProjectRepo;
using TaskProject.Repository.TaskRepo;
using TaskProject.Service.Project;
using TaskProject.ViewModels.Project;
using TaskProject.ViewModels.SubTask;
using TaskProject.ViewModels.Tasks;
using TaskProject.ViewModels.UserVM;
using Task = TaskProject.Models.Task;


namespace TaskProject.Service.TaskServ
{
    public class TaskService : Service<Task>, ITaskService
    {
        private readonly ITaskRepository taskRepository;

        public TaskService(ITaskRepository _taskRepository)
        {
            this.taskRepository = _taskRepository;
        }

        public async Task<bool> InsertTask(TaskFormFileViewModel taskFormFileView)
        {
            Task t = new Task
            {
                Name = taskFormFileView.FormFile_TaskName,
                Description = taskFormFileView.FormFile_TaskDescription,
                Attachment = await FileHelper.SaveFileAsync(taskFormFileView.FormFile_TaskAttachment),
                DueDate = taskFormFileView.FormFile_TaskDueDate,
                ProjectId = taskFormFileView.FormFile_ProjectId,
                Subtasks = taskFormFileView.FormFile_TaskSubtasks.Select(p => new Subtask
                {
                    Id = p.SubTaskId,
                    Description = p.SubTaskDescription,
                    DueDate = p.SubTaskDueDate,
                    Name = p.SubTaskName
                }).ToList()
            };
            if (t != null)
            {
                await taskRepository.InsertTaskAsync(t);
                return true;
            }
            return false;
        }




        public async Task<TaskViewModel> GetSpecificTask(int id)
        {
            Task task = await taskRepository.GetSpecificAsync(id);
            List<ApplicationUser> users = await taskRepository.GetUsersOfTask(id);

            if (task != null || users != null)
            {
                TaskViewModel taskViewModel = new TaskViewModel
                {
                    TaskId = task.Id,
                    TaskName = task.Name,
                    TaskAttachment = task.Attachment,
                    TaskDueDate = task.DueDate,
                    TaskDescription = task.Description,
                    TaskSubtasks = task.Subtasks.Select(p => new SubTaskViewModel
                    {
                        SubTaskId = p.Id,
                        SubTaskDescription = p.Description,
                        SubTaskDueDate = p.DueDate,
                        SubTaskName = p.Name,
                        
                    }).ToList()
                    ,TaskAppUsers= users.Select(u=>new AppUserVM
                    {
                        idVM = u.Id,
                        UsernameVM=u.UserName,
                        FirstNameVM=u.FirstName,
                        LastNameVM=u.LastName,
                        EmailVM=u.Email,
                  
                    }).ToList()
                };
                return taskViewModel;
            }

            return null;
        }

        public async Task<List<TaskViewModel>> GetAllTasks()
        {

            List<Task> tasks = await taskRepository.GetAllAsync();

            if (tasks != null || tasks.Count == 0)
            {
                List<TaskViewModel> taskViewModels = tasks.Select(t => new TaskViewModel
                {
                    TaskId = t.Id,
                    TaskName = t.Name,
                    TaskDescription = t.Description, 
                    TaskDueDate = t.DueDate,
                    TaskAttachment=t.Attachment,
                    TaskIsDeleted = t.IsDeleted,
                    TaskSubtasks = t.Subtasks.Select(p => new SubTaskViewModel
                    {
                        SubTaskId = p.Id,
                        SubTaskDescription = p.Description,
                        SubTaskDueDate = p.DueDate,
                        SubTaskName = p.Name,
                        
                    }).ToList()
                }).ToList();




                return taskViewModels;
            }

            return new List<TaskViewModel>();
        }


        public async Task<Task> UpdateTask(TaskViewModel taskViewModel)
        {
            Task task = await taskRepository.GetSpecificAsync(taskViewModel.TaskId);

            if (task != null )
            {
                task.Name = taskViewModel.TaskName;
                task.Description = taskViewModel.TaskDescription;
                task.DueDate = taskViewModel.TaskDueDate;
                task.Attachment = taskViewModel.TaskAttachment;
                task.IsDeleted = taskViewModel.TaskIsDeleted;


                taskRepository.Update(task);

                return task;
            }

            return null; // Consider handling this case appropriately in your controller
        }

        public async Task<bool> AssignTask(TaskViewModel taskViewModel, int ProjectId)
        {
            Task task = await taskRepository.GetSpecificAsync(taskViewModel.TaskId);
            if (task != null)
            {
                 task.ProjectId = ProjectId;

               taskRepository.Update(task);
                return true;

            }
            return false;
        }

        public async Task<bool> AssignUserTask(int TaskID, string UserId)
        {
            Task task = await taskRepository.GetSpecificAsync(TaskID);
            //ApplicationUser applicationUser = 
            if (task != null)
            {


                return true;
            }
            return false;
        }

        public async Task<List<TaskViewModel>> GetTasksByUserId(string userId)
        {
            var tasks = await taskRepository.GetTasksByUserId(userId);
            return tasks.Select(t => new TaskViewModel
            {
                TaskId = t.Id,
                TaskName = t.Name,
                TaskDescription = t.Description,
                TaskDueDate = t.DueDate
            }).ToList();
        }
    }
}

