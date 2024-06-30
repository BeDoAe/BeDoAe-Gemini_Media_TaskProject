using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskProject.ViewModels.RoleVM;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskProject.Controllers
{
    [Authorize(Roles ="Admin")]
    //Admin
    //@Admin12345
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name=roleViewModel.RoleName,
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return  View("Role_Added");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("AddRole");
        }
    }
}
