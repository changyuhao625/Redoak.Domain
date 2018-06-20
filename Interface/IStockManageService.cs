using System.Threading.Tasks;
using KendoGridBinder;
using Redoak.Domain.Model.Dto;
using Redoak.Domain.Model.Models;

namespace Redoak.Domain.Interface
{
    public interface IStockManageService
    {
        Task CreateOrEdit(Goods model);

        Task<KendoGrid<Goods>> Query(StockQueryDto dto);

        Task<Goods> Edit(int id);
    }
}