namespace TaskProject.Repository.ProjectRepo
{
    public interface IProjectRepository : IRepository<Project>
    {
        public  Task<List<Project>> GetAllAsync();

        public  Task<Project> GetSpecificAsync(int id);

        //public  void DeleteProject(int id);
        public  Task<bool> DeleteProject(int id);

        public Task<bool> DeleteProjectWithStoredProcedure(int projectId);




    }
}