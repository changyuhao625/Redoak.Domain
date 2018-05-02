using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Redoak.Core.Cache.Interface;
using Redoak.Domain.Model.Enum;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Cache
{
    public class CacheService : ICacheService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CacheService(IMemoryCache cache, RedoakContext context, RoleManager<IdentityRole> roleManager)
        {
            Cache = cache;
            Context = context;
            _roleManager = roleManager;
        }

        private IMemoryCache Cache { get; }
        private RedoakContext Context { get; }

        public async Task<IList<IdentityRole>> Roles()
        {
            if (!Cache.TryGetValue(RedoakEnum.Cache.Role, out var role))
            {
                role = await _roleManager.Roles.ToListAsync();
                Cache.CreateEntry(RedoakEnum.Cache.Role).Value = role;
                return (IList<IdentityRole>) role;
            }

            return (IList<IdentityRole>) role;
        }
    }
}