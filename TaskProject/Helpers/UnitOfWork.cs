using TaskProject.Repository.DueDateRepo;
using TaskProject.Repository.ProjectRepo;
using TaskProject.Repository.TaskRepo;
using TaskProject.Repository.UserRepo;
using TaskProject.Service.Project;
using TaskProject.Service.TaskServ;
using TaskProject.Service.UserServ;

namespace TaskProject.Helpers
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Context _context;

        public IProjectRepository ProjectRepository { get;  }

        public IProjectService ProjectService { get;  }

        public ITaskRepository TaskRepository { get; }

        public ITaskService TaskService { get; }

        public IUserRepository UserRepository { get; }

        public IUserService UserService { get; }

        public IDueDateRepository DueDateRepository { get; }


        public UnitOfWork(Context context , IProjectRepository projectRepository , IProjectService projectService
            ,ITaskService taskService, ITaskRepository taskRepository , IUserRepository userRepository , IUserService userService ,IDueDateRepository dueDateRepository
            )
        {
            this._context = context;
            this.ProjectRepository = projectRepository;
            this.ProjectService = projectService;
            this.TaskRepository = taskRepository;
            this.TaskService = taskService;
            this.UserRepository = userRepository;
            this.UserService = userService;
            this.DueDateRepository = dueDateRepository;


        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
