using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Redoak.Domain.Interface;
using Redoak.Domain.Model.Models;
using Redoak.Domain.Model.ViewModel;

namespace Redoak.Domain.Service
{
    public class ManageService : BaseService, IManageService
    {
        public IUserService UserService;
        public UserManager<ApplicationUser> UserManager;
        public RoleManager<IdentityRole> RoleManager;

        public ManageService(RedoakContext context, IUserService userService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            UserService = userService;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<(ApplicationUser userInfo, IList<string> Roles)> GetEditUserAsync(string userId)
        {
            var user = await this.UserManager.FindByIdAsync(userId);
            return (await this.UserManager.FindByIdAsync(userId),
                await this.UserManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> SaveUser(string userId, IList<string> userRoles)
        {
            var user = await this.UserManager.FindByIdAsync(userId);
            var preventRoles = await this.UserManager.GetRolesAsync(user);
            await this.UserManager.RemoveFromRolesAsync(user, preventRoles);
            return await this.UserManager.AddToRolesAsync(user, userRoles);
        }
    }
}