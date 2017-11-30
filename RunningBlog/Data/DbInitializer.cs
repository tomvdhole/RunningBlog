using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RunningBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var runningBlogDbContext = serviceProvider.GetRequiredService<RunningBlogDbContext>();
            runningBlogDbContext.Database.EnsureCreated();

            await CreationOfRoles(serviceProvider);
            await CreationOfUsers(serviceProvider);
        }

        #region Private Methods
        private static async Task CreationOfRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            IOptions<InitializationOptions> optionsAccessor = serviceProvider.GetRequiredService<IOptions<InitializationOptions>>();
            InitializationOptions options = optionsAccessor.Value;

            foreach (var roleName in options.UserNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task CreationOfUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IOptions<InitializationOptions> optionsAccessor = serviceProvider.GetRequiredService<IOptions<InitializationOptions>>();
            InitializationOptions options = optionsAccessor.Value;

            int userCounter = -1;
            foreach (string user in options.UserNames)
            {
                userCounter++;
                var _user = await userManager.FindByNameAsync(user);
                if (_user == null)
                {
                    var appUser = new ApplicationUser
                    {
                        UserName = user
                    };
                    var creationOfUser = await userManager.CreateAsync(appUser, options.PassWords[userCounter]);
                    if (creationOfUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(appUser, options.UserNames[userCounter]);
                    }
                }
            }
        }
        #endregion Private Methods
    }
}
