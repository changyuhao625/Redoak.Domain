using System.Threading.Tasks;
using Redoak.Domain.Interface;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Service
{
    public class CustomerManageService : BaseService, ICustomerManageService
    {
        public CustomerManageService(RedoakContext context) : base(context)
        {
        }

        public async Task Create(Customer model)
        {
            await Context.Customer.AddAsync(model);
        }
    }
}