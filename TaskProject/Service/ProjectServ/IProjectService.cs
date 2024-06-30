using TaskProject.Models;
using TaskProject.Repository;
using TaskProject.ViewModels.Project;
using project = TaskProject.Models.Project;
using Task = TaskProject.Models.Task;

namespace TaskProject.Service.Project
{
    public interface IProjectService : IService<project>
    {
        //public void InsertProject(ProjectViewModel projectViewModel
        public  Task<bool> InsertProject(ProjectViewModel projectViewModel);


        public Task<ProjectViewModel> GetSpecificProject(int id);

        public Task<List<ProjectViewModel>> GetAllProjects();

        public  Task<project> UpdateProject(ProjectViewModel projectViewModel);
    }
}