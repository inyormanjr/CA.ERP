using AutoMapper;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.GetManyBranch
{
    public class GetManyBranchHandler : IRequestHandler<GetManyBranchQuery, PaginatedResponse<BranchView>>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public GetManyBranchHandler(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<BranchView>> Handle(GetManyBranchQuery request, CancellationToken cancellationToken)
        {
            var branches = await _branchRepository.GetManyAsync(request.Skip, request.Take, request.Status, cancellationToken: cancellationToken);
            var total = await _branchRepository.GetCountAsync(request.Status, cancellationToken: cancellationToken);
            return new PaginatedResponse<BranchView>()
            {
                Data = _mapper.Map<List<BranchView>>(branches),
                TotalCount = total
            };
        }
    }
}
