using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Redoak.Domain.Cache
{
    public interface ICacheService
    {
        Task<IList<IdentityRole>> Roles();
    }
}