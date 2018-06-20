using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KendoGridBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Redoak.Domain.Interface;
using Redoak.Domain.Model.Dto;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Service
{
    public class StockManageService : BaseService, IStockManageService
    {
        public StockManageService(RedoakContext context) : base(context)
        {
        }

        public async Task<KendoGrid<Goods>> Query(StockQueryDto dto)
        {
            var queryable = Context.Goods.Include(x => x.Category).AsQueryable();
            //.Skip(dto.Skip ?? 0)
            //.Take(dto.Take ?? int.MaxValue);
            if (dto.Id > 0) queryable = queryable.Where(x => x.Id == dto.Id);

            if (!string.IsNullOrEmpty(dto.Name)) queryable = queryable.Where(x => x.Name == dto.Name);

            if (dto.CategoryId > 0) queryable = queryable.Where(x => x.Category.Id == dto.CategoryId);

            var count = await queryable.CountAsync();
            var result = await queryable.Skip(dto.Skip ?? 0).Take(dto.Take ?? 0).ToListAsync();
            var grid = new KendoGrid<Goods>(result, count);
            return grid;
        }

        public async Task CreateOrEdit(Goods model)
        {
            var findSame = await Context.Goods.CountAsync(x => x.Name == model.Name.Trim());
            if (findSame > 1)
            {
                throw new Exception($"商品名稱:{model.Name} ,已存在!");
            }

            if (model.Id > 0)
            {
                var goods = await Context.Goods.SingleAsync(x => x.Id == model.Id);
                goods.Name = model.Name;
                goods.UpdateDate = DateTime.Now;
                Context.Goods.Update(goods);
            }
            else
            {
                await Context.Goods.AddAsync(model);
            }

            await Context.SaveChangesAsync();
        }

        public async Task<Goods> Edit(int id)
        {
            return await Context.Goods.Include(x => x.Category).SingleAsync(x => x.Id == id);
        }
    }
}