using System.ComponentModel.DataAnnotations.Schema;

namespace TaskProject.ViewModels.UserVM
{
    public class UserTaskVM
    {
        public int IdVM { get; set; }
        public bool IsDeletedVM { get; set; } = false;


        public int TaskIdVM { get; set; }

        public string TaskNameVM { get; set; }

        public string UserIdVM { get; set; }

    }
}
