using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Blazor.Inferfaces.Services
{
    public interface ICreateService<TCreate> where TCreate : class 
    {
        Task<Guid> CreateAsync(TCreate dto);
    }
}
