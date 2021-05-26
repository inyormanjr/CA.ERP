using CA.ERP.Shared.Dto.StockTransfer;
using CA.ERP.WebApp.Blazor.Inferfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Services
{
    public interface IStockTransferService: ICreateService<StockTransferCreate>
    {

    }

    public class StockTransferService : ServiceBase<StockTransferCreate>, IStockTransferService
    {
        public StockTransferService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "api/StockTransfer")
        {
        }
    }
}
