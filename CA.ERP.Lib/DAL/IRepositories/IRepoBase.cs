using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Lib.DAL.IRepositories
{
    public interface IRepoBase<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(int skip, int take);
        Task<T> GetById(int id);

        Task<bool> SaveAll();
    }
}
