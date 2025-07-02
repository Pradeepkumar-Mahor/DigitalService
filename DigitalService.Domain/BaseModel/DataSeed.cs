using DigitalService.Domain.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace DigitalService.Domain.BaseModel
{
    public static class DataSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Seed database code goes here

                // User Info
                //string userName = "SuperAdmin";
                string firstName = "Pradeep";
                string lastName = "Mahot";
                string email = "Pradeepkumar.Mahor@sensiaglobal.com";
                string password = "Dundas#Sensia2025";

                string roleSuperAdmin = "SuperAdmin";
                string roleAdmin = "Admin";
                string roleUser = "User";

                if (await _userManager.FindByNameAsync(email) == null)
                {
                    // Create SuperAdmins role if it doesn't exist
                    if (await roleManager.FindByNameAsync(roleSuperAdmin) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleSuperAdmin));
                    }
                    if (await roleManager.FindByNameAsync(roleAdmin) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleAdmin));
                    }
                    if (await roleManager.FindByNameAsync(roleUser) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleUser));
                    }

                    // Create user account if it doesn't exist
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,

                        //extended properties
                        FirstName = firstName,
                        LastName = lastName,
                        AvatarURL = "/images/user.png",
                        DateRegistered = DateTime.UtcNow.ToString(),
                        Position = "Lead",
                        NickName = "Pradeepkumar",
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, password);

                    // Assign role to the user
                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.GivenName, user.FirstName));
                        await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Surname, user.LastName));
                        await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.AvatarURL, user.AvatarURL));

                        //SignInManager<ApplicationUser> _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
                        //await _signInManager.SignInAsync(user, isPersistent: false);

                        await _userManager.AddToRoleAsync(user, roleSuperAdmin);
                    }
                }
            }
        }
    }
}