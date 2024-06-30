using TaskProject.Repository.ProjectRepo;
using TaskProject.Repository.TaskRepo;
using TaskProject.Service.Project;
using TaskProject.Service.TaskServ;

namespace TaskProject.Helpers
{
    public interface IUnitOfWork
    {
        public void Save();

        public IProjectRepository ProjectRepository { get; }

        public IProjectService ProjectService { get; }



        public ITaskRepository TaskRepository { get; }

        public ITaskService TaskService { get; }


    }
}