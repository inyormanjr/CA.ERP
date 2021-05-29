using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Inferfaces.Services
{
    public interface IGetByIdService<TView> where TView : class
    {
        Task<TView> GetById(Guid id);
    }
}
