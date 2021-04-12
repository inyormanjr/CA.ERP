using System.Collections.Generic;
using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.RebateMonthlyAgg
{
    public interface IRebateMonthlyRepository : IRepository
    {
        List<RebateMonthly> GetRebateMonthlyList();
    }
}