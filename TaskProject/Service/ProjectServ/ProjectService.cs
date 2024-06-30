using System.Threading.Tasks;
using TaskProject.Models;
using TaskProject.Repository.ProjectRepo;
using TaskProject.ViewModels.Project;
using TaskProject.ViewModels.SubTask;
using TaskProject.ViewModels.Tasks;
using project = TaskProject.Models.Project;
using Task = TaskProject.Models.Task;


namespace TaskProject.Service.Project
{
    public class ProjectService : Service<project>, IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task <bool>  InsertProject(ProjectViewModel projectViewModel)
        {
            var project = new project
            {
                Description = projectViewModel.ProjectDescription,
                Name = projectViewModel.ProjectName,
                Tasks = new List<Task>()
                //Tasks = projectViewModel.ProjectTasks.Select(tv => new Task
                //{
                //    Description = tv.TaskDescription,
                //    DueDate = tv.TaskDueDate,
                //    Name = tv.TaskName,
                //    Attachment = tv.TaskAttachment,
                //    Subtasks = tv.TaskSubtasks.Select(stv => new Subtask
                //    {
                //        Name = stv.SubTaskName,
                //        Description = stv.SubTaskDescription,
                //        DueDate = stv.SubTaskDueDate
                //    }).ToList()
                //}).ToList()
            };

        bool result=  await  projectRepository.InsertAsync(project);
            if (result)
            {
                return true;
            }
            return false;
        }


        public async Task< ProjectViewModel> GetSpecificProject(int id)
        {
            project project = await projectRepository.GetSpecificAsync(id);
            if (project != null)
            {
                ProjectViewModel projectViewModel = new ProjectViewModel
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                    ProjectDescription = project.Description, // Consider renaming this to "ProjectDescription" for consistency
                    ProjectTasks = project.Tasks.Select(p => new TaskViewModel
                    {
                        TaskId = p.Id,
                        TaskDescription = p.Description,
                        TaskDueDate = p.DueDate,
                        TaskName = p.Name,
                        TaskAttachment = p.Attachment,
                        TaskSubtasks = p.Subtasks.Select(stv => new SubTaskViewModel
                        {
                            SubTaskName = stv.Name,
                            SubTaskDescription = stv.Description,
                            SubTaskDueDate = stv.DueDate,
                            SubTaskId = stv.TaskId,

                        }).ToList()
                    }).ToList()
                };

                return projectViewModel;
            }

            return null;
        }

        public async Task< List<ProjectViewModel>> GetAllProjects()
           
        {
            List<project> projects =await projectRepository.GetAllAsync();

            if (projects != null)
            {
                List<ProjectViewModel> projectViewModels = projects.Select(project => new ProjectViewModel
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                    ProjectDescription = project.Description, // Consider renaming this to "ProjectDescription"
                    ProjectTasks = project.Tasks.Select(p => new TaskViewModel
                    {
                        TaskId = p.Id,
                        TaskDescription = p.Description,
                        TaskDueDate = p.DueDate,
                        TaskName = p.Name,
                        TaskAttachment = p.Attachment,
                        TaskSubtasks = p.Subtasks.Select(stv => new SubTaskViewModel
                        {
                            SubTaskName = stv.Name,
                            SubTaskDescription = stv.Description,
                            SubTaskDueDate = stv.DueDate,
                            SubTaskId = stv.TaskId,

                        }).ToList()
                    }).ToList()
                }).ToList();
                
                


                return projectViewModels;
            }

            return new List<ProjectViewModel>();
        }

        //public async Task< project> UpdateProject(ProjectViewModel projectViewModel)
        //{
        //    project project = await projectRepository.GetSpecificAsync(projectViewModel.ProjectId);
        //    if (project != null)
        //    {

        //        project.Name = projectViewModel.ProjectName;
        //        project.Description = projectViewModel.PtojectDescription;
        //        //project.Tasks = projectViewModel.ProjectTasks.Select(tv => new Task
        //        //{
        //        //    Id = tv.TaskId,
        //        //    Name = tv.TaskName,
        //        //    Description = tv.TaskDescription,
        //        //    DueDate = tv.TaskDueDate,
        //        //    Attachment = tv.TaskAttachment,
        //        //    Subtasks = tv.TaskSubtasks.Select(stv => new Subtask
        //        //    {
        //        //        Name = stv.Name,
        //        //        Description = stv.Description,
        //        //        DueDate = stv.DueDate,
        //        //        TaskId = stv.TaskId,

        //        //    }).ToList()
        //        //}).ToList();


        //    projectRepository.Update(project);
        //        return project;
        //    }
        //    return new project();
        //}

        public async Task<project> UpdateProject(ProjectViewModel projectViewModel)
        {
            project project = await projectRepository.GetSpecificAsync(projectViewModel.ProjectId);
            if (project != null)
            {
                project.Name = projectViewModel.ProjectName;
                project.Description = projectViewModel.ProjectDescription;

                projectRepository.Update(project);

                return project;
            }

            return null; // Consider handling this case appropriately in your controller
        }




    }
}

