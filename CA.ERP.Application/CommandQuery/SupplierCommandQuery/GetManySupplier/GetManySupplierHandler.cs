using AutoMapper;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Supplier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplier
{
    public class GetManySupplierHandler : IRequestHandler<GetManySupplierQuery, PaginatedResponse<SupplierView>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public GetManySupplierHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<SupplierView>> Handle(GetManySupplierQuery request, CancellationToken cancellationToken)
        {
            int count = await _supplierRepository.GetCountSupplierAsync(request.Name, cancellationToken);
            var suppliers = await _supplierRepository.GetManySupplierAsync(request.Name, request.Skip, request.Take, cancellationToken);

            return new PaginatedResponse<SupplierView>(_mapper.Map<List<SupplierView>>(suppliers), count);
        }
    }
}
