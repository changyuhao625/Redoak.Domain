using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Redoak.Domain.Model.ViewModel;

namespace Redoak.Domain.Interface
{
    public interface IUserRoleService
    {
        Task<(ApplicationUser userInfo, IList<string> Roles)> GetEditUserAsync(string userId);

        Task<IdentityResult> SaveUser(string userId, IList<string> userRoles);
    }
}