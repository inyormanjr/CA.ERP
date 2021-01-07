using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<bool> StockNumberExist(string stockNumber, Guid exludeId = default);
        Task<bool> SerialNumberExist(string serialNumber, Guid exludeId = default);
    }
}
