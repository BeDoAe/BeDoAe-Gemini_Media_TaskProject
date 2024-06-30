namespace TaskProject.ViewModels.UserVM
{
    public class AppUserVM
    {
       public string idVM { get; set; }
        public string UsernameVM { get; set; }
        public string FirstNameVM { get; set; }
        public string LastNameVM { get; set; }

        public string EmailVM { get; set; }
        public List<UserTaskVM>? UserTasksVM { get; set; } 
    }
}
