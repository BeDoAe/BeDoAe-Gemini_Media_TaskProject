using System.ComponentModel.DataAnnotations;

namespace TaskProject.ViewModels.UserVM
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Username is required ")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Username is invalid, can only contain letters or digits as Bedo or Bedo123")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 5)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string LastName { get; set; }
    }
}