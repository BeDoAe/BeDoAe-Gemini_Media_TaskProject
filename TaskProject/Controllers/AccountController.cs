using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using TaskProject.ViewModels.UserVM;

namespace TaskProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                // Save user
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = userVM.UserName,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    Email = userVM.Email,
                };
                IdentityResult identityResult = await userManager.CreateAsync(applicationUser, userVM.Password);
                if (identityResult.Succeeded)
                {
                    //Making a user as an Admin as a Role
                IdentityResult RoleResult  = await userManager.AddToRoleAsync(applicationUser, "Admin");
                    if(RoleResult.Succeeded==false)
                    {
                        foreach (var error in RoleResult.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                    // Cookie --> signInManager
                    await signInManager.SignInAsync(applicationUser, false);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Register", userVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid)
            {
                //check if user existed

             ApplicationUser applicationUser=  await userManager.FindByNameAsync(loginUserViewModel.UserName);

                if (applicationUser != null)
                {
                    //check password
                bool isCorrect = await  userManager.CheckPasswordAsync(applicationUser,loginUserViewModel.Password);

                    if (isCorrect)
                    {

                        //Add extra info to cookie
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Gemini_Employees", "Welcome"));

                        //Create cookie
                        await signInManager.SignInWithClaimsAsync(applicationUser, loginUserViewModel.RememberMe, claims);

                        //await signInManager.SignInAsync(applicationUser, loginUserViewModel.RememberMe);

                        //Get Info from cookie about Login user
                        string userName = User.Identity.Name;
                        Claim claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        if (claimId != null)
                        {
                            string userId = claimId.Value;
                        }

                        return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError("", "Invalid Account");





            }
            return View("Login" ,loginUserViewModel);
        }
        // /Account/Logout
        public async Task<IActionResult> Logout()
        {
         
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
