using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Base
{
    public interface IRepository {
    
    }
    public interface IRepository<T> : IRepository
    {
        void Insert(T entity);
        void Delete(string id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(int skip, int take);
        Task<T> GetById(string id);

        Task<bool> SaveAll();
    }
}
