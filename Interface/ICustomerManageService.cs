using KendoGridBinder;
using Redoak.Domain.Model.Dto;
using Redoak.Domain.Model.Models;
using System.Threading.Tasks;

namespace Redoak.Domain.Interface
{
    public interface ICustomerManageService
    {
        Task CreateOrEdit(Customer model);

        Task<KendoGrid<Customer>> Query(CustomerQueryDto dto);

        Task<Customer> Edit(int id);
    }
}