using CommunityOfMars.Data;
using CommunityOfMars.Models;
using Microsoft.AspNetCore.Identity;

namespace CommunityOfMars
{
    public class SeedData
    {
        public static void Seed(MarsDBContext context, IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
			IdentityRole adminRole = roleManager.FindByNameAsync("Admin").Result;
			if (adminRole == null)
            {
				_ = roleManager.CreateAsync(new IdentityRole("Admin")).Result.Succeeded;
			}
			if (!context.Messages.Any())
            {
                const string SECRET_PASSWORD = "P4ssw0rd!";
                AppUser system = new AppUser { UserName = "System" };
                var result = userManager.CreateAsync(system, SECRET_PASSWORD).Result.Succeeded;
                _ = userManager.AddToRoleAsync(system, "Admin").Result.Succeeded;
				AppUser all = new AppUser { UserName = "All" };
                result &= userManager.CreateAsync(all, SECRET_PASSWORD).Result.Succeeded;
                if (result)
                {
                    Message message = new()
                    {
                        Sender = system,
                        Receiver = all,
                        Title = "Welcome!",
                        Body = "Welcome to the message board! Feel free to send messages here!",
                        Priority = 0,
                        Date = DateOnly.Parse("11/11/1111")
                    };
                    context.Messages.Add(message);
                }
                context.SaveChanges();
            }
        }
    }
}
