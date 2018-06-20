using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Redoak.Domain.Interface;
using Redoak.Domain.Model.Models;
using Redoak.Domain.Model.ViewModel;

namespace Redoak.Domain.Service
{
    public class UserRoleService : BaseService, IUserRoleService
    {
        public RoleManager<IdentityRole> RoleManager;
        public UserManager<ApplicationUser> UserManager;
        public IUserService UserService;

        public UserRoleService(RedoakContext context, IUserService userService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            UserService = userService;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<(ApplicationUser userInfo, IList<string> Roles)> GetEditUserAsync(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            return (await UserManager.FindByIdAsync(userId),
                await UserManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> SaveUser(string userId, IList<string> userRoles)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var preventRoles = await UserManager.GetRolesAsync(user);
            await UserManager.RemoveFromRolesAsync(user, preventRoles);
            return await UserManager.AddToRolesAsync(user, userRoles);
        }
    }
}