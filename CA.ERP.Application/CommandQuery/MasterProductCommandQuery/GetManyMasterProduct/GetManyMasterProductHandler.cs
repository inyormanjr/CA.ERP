using AutoMapper;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.MasterProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetManyMasterProduct
{
    public class GetManyMasterProductHandler : IRequestHandler<GetManyMasterProductQuery, PaginatedResponse<MasterProductView>>
    {
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IMapper _mapper;

        public GetManyMasterProductHandler(IMasterProductRepository masterProductRepository, IMapper mapper)
        {
            _masterProductRepository = masterProductRepository;
            _mapper = mapper;
        }


        public async Task<PaginatedResponse<MasterProductView>> Handle(GetManyMasterProductQuery request, CancellationToken cancellationToken)
        {
            var masterProducts = await _masterProductRepository.GetManyAsync(request.Model, request.Skip, request.Take, request.Status, cancellationToken: cancellationToken);
            var count = await _masterProductRepository.GetCountAsync(request.Model, request.Status, cancellationToken: cancellationToken);

            return new PaginatedResponse<MasterProductView>
            {
                Data = _mapper.Map<List<MasterProductView>>(masterProducts),
                TotalCount = count
            };
        }
    }
}
