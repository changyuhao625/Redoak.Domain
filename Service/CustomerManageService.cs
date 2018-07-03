using KendoGridBinder;
using Microsoft.EntityFrameworkCore;
using Redoak.Domain.Interface;
using Redoak.Domain.Model.Dto;
using Redoak.Domain.Model.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Redoak.Domain.Service
{
    public class CustomerManageService : BaseService, ICustomerManageService
    {
        public CustomerManageService(RedoakContext context) : base(context)
        {
        }

        public async Task<KendoGrid<Customer>> Query(CustomerQueryDto dto)
        {
            var queryable = Context.Customer.Include(x => x.Region).AsQueryable();
            if (!string.IsNullOrEmpty(dto.Name)) queryable = queryable.Where(x => x.Name.Contains(dto.Name));
            var count = await queryable.CountAsync();
            var result = await queryable.Skip(dto.Skip ?? 0).Take(dto.Take ?? int.MaxValue).ToListAsync();
            var grid = new KendoGrid<Customer>(result, count);
            return grid;
        }

        public async Task<Customer> Edit(int id)
        {
            var customer = await Context.Customer.Include(x => x.Region).SingleOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                throw new Exception($"Cann't  find the customer which id is '{id}'!");
            }

            return customer;
        }

        public async Task CreateOrEdit(Customer model)
        {
            //Edit
            if (model.Id > 0)
            {
                var customer = await Context.Customer.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (customer != null)
                {
                    customer.Name = model.Name;
                    customer.Address = model.Address;
                    customer.ContactEmail = model.ContactEmail;
                    customer.RegionId = model.RegionId;
                    customer.ContactPerson = model.ContactPerson;
                    customer.ContactPhone = model.ContactPhone;
                    customer.UpdateDate = DateTime.Now;
                    customer.UpdateUser = model.UpdateUser;
                    Context.Customer.Update(customer);
                }
                else
                {
                    throw new Exception($"Id:{model.Id},客戶不存在!");
                }
            }
            else
            {
                //Todo: add Region Condition
                var isRegionExist = await Context.Region.CountAsync(x => x.Id == 1) == 1;
                if (isRegionExist)
                {
                    var region = await Context.Region.Include(x => x.Customers).FirstAsync(x => x.Id == 1);
                    model.CrateDate = DateTime.Now;
                    model.UpdateDate = DateTime.Now;
                    region.Customers.Add(model);
                }
                else
                {
                    throw new Exception($"RegionId:{model.RegionId} is not a valid value!");
                }
            }

            await Context.SaveChangesAsync();
        }
    }
}