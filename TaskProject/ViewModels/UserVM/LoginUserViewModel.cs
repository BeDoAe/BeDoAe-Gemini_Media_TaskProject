using System.ComponentModel.DataAnnotations;

namespace TaskProject.ViewModels.UserVM
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Username is required ")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Username is invalid, can only contain letters or digits as Bedo or Bedo123")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 5)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
