using CommunityOfMars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommunityOfMars.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private UserManager<AppUser> userManager;
		private RoleManager<IdentityRole> roleManager;

		public UserController(UserManager<AppUser> userMngr,
			RoleManager<IdentityRole> roleMngr)
		{
			userManager = userMngr;
			roleManager = roleMngr;
		}

		public async Task<IActionResult> Index()
		{
			List<AppUser> users = new List<AppUser>();
			foreach (AppUser user in userManager.Users.ToList())
			{
				user.RoleNames = await userManager.GetRolesAsync(user);
				users.Add(user);
			}
			UserViewModel model = new UserViewModel
			{
				Users = users,
				Roles = roleManager.Roles
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			AppUser user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await userManager.DeleteAsync(user);
				if (!result.Succeeded) // if failed
				{
					string errorMessage = "";
					foreach (IdentityError error in result.Errors)
					{
						errorMessage += error.Description + " | ";
					}
					TempData["message"] = errorMessage;
				}
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View("../Account/Register");
		}

		[HttpPost]
		public async Task<IActionResult> Add(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUser { UserName = model.Username };
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View("../Account/Register", model);
		}

		[HttpPost]
		public async Task<IActionResult> AddToAdmin(string id)
		{
			IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
			if (adminRole == null)
			{
				TempData["message"] = "Admin role does not exist. "
					+ "Click 'Create Admin Role' button to create it.";
			}
			else
			{
				AppUser user = await userManager.FindByIdAsync(id);
				await userManager.AddToRoleAsync(user, adminRole.Name);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromAdmin(string id)
		{
			AppUser user = await userManager.FindByIdAsync(id);
			var result = await userManager.RemoveFromRoleAsync(user, "Admin");
			if (result.Succeeded) { }
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(string id)
		{
			IdentityRole role = await roleManager.FindByIdAsync(id);
			var result = await roleManager.DeleteAsync(role);
			if (result.Succeeded) { }
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> CreateAdminRole()
		{
			var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
			if (result.Succeeded) { }
			return RedirectToAction("Index");
		}
	}
}
