using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redoak.Domain.Interface;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(RedoakContext context) : base(context)
        {
        }

        public async Task<IList<AspNetUsers>> GetUser()
        {
            return await Context.AspNetUsers.ToListAsync();
        }

        public async Task<AspNetUsers> GetUser(string id)
        {
            return await Context.AspNetUsers.FindAsync(id);
        }
    }
}