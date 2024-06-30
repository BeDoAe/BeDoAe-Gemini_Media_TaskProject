using TaskProject.Repository.DueDateRepo;
using TaskProject.Repository.ProjectRepo;
using TaskProject.Repository.TaskRepo;
using TaskProject.Repository.UserRepo;
using TaskProject.Service.Project;
using TaskProject.Service.TaskServ;
using TaskProject.Service.UserServ;

namespace TaskProject.Helpers
{
    public interface IUnitOfWork
    {
        public void Save();

        public IProjectRepository ProjectRepository { get; }

        public IProjectService ProjectService { get; }



        public ITaskRepository TaskRepository { get; }

        public ITaskService TaskService { get; }

        public IUserRepository UserRepository { get; }

        public IUserService UserService { get; }


        public IDueDateRepository DueDateRepository { get; }



    }
}