using TaskProject.Repository.ProjectRepo;
using TaskProject.Repository.TaskRepo;
using TaskProject.Service.Project;
using TaskProject.Service.TaskServ;

namespace TaskProject.Helpers
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Context _context;

        public IProjectRepository ProjectRepository { get;  }

        public IProjectService ProjectService { get;  }

        public ITaskRepository TaskRepository { get; }

        public ITaskService TaskService { get; }


        public UnitOfWork(Context context , IProjectRepository projectRepository , IProjectService projectService
            ,ITaskService taskService, ITaskRepository taskRepository
            )
        {
            this._context = context;
            this.ProjectRepository = projectRepository;
            this.ProjectService = projectService;
            this.TaskRepository = taskRepository;
            this.TaskService = taskService;


        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
