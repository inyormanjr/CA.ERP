using AutoMapper;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Brand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.GetManyBrand
{
    public class GetManyBrandHandler : IRequestHandler<GetManyBrandQuery, PaginatedResponse<BrandView>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetManyBrandHandler( IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<BrandView>> Handle(GetManyBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetManyAsync(request.Skip, request.Take, request.Status, cancellationToken: cancellationToken);
            var count = await _brandRepository.GetCountAsync(request.Status, cancellationToken: cancellationToken);

            return new PaginatedResponse<BrandView>() {
                Data = _mapper.Map<List<BrandView>>(brands.OrderBy(b => b.Name)),
                TotalCount = count
            };
        }
    }
}
