using CA.ERP.Domain.UnitOfWorkAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CADataContext _context;

        public UnitOfWork(CADataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// save all changes to database
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {

            _context.Dispose();
        }
    }
}
