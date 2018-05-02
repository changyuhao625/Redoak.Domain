using System.Collections.Generic;
using System.Threading.Tasks;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Interface
{
    public interface IUserService
    {
        Task<IList<AspNetUsers>> GetUser();
        Task<AspNetUsers> GetUser(string id);
    }
}