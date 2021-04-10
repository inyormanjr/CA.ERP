using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.SupplierAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplier
{
    public class GetManySupplierHandler : IRequestHandler<GetManySupplierQuery, PaginatedList<Supplier>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierRepository _supplierRepository;

        public GetManySupplierHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository)
        {
            _unitOfWork = unitOfWork;
            _supplierRepository = supplierRepository;
        }

        public async Task<PaginatedList<Supplier>> Handle(GetManySupplierQuery request, CancellationToken cancellationToken)
        {
            int count = await _supplierRepository.GetCountSupplierAsync(request.Name, cancellationToken);
            var suppliers = await _supplierRepository.GetManySupplierAsync(request.Name, request.Skip, request.Take, cancellationToken);

            return new PaginatedList<Supplier>(suppliers, count);
        }
    }
}
