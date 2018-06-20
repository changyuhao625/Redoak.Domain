using System.Threading.Tasks;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Interface
{
    public interface ICustomerManageService
    {
        Task Create(Customer model);
    }
}