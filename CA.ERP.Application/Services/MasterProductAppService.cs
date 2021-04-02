using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.Services
{
    public interface IMasterProductAppService : IAppService
    {
        Task<DomainResult<Guid>> CreateMasterProduct(string model, string description, Guid brandId, CancellationToken cancellationToken = default);
        Task<DomainResult> UpdateAsync(Guid id, string model, string description, Guid brandId, ProductStatus productStatus, CancellationToken cancellationToken);
        Task<List<MasterProduct>> GetManyAsync(CancellationToken cancellationToken);
        Task<DomainResult<MasterProduct>> GetOneAsync(Guid id, CancellationToken cancellationToken);
    }
    public class MasterProductAppService : IMasterProductAppService
    {
        private readonly MasterProductService _masterProductService;

        public MasterProductAppService(MasterProductService masterProductService)
        {
            _masterProductService = masterProductService;
        }
        public Task<DomainResult<Guid>> CreateMasterProduct(string model, string description, Guid brandId, CancellationToken cancellationToken = default)
        {
            return _masterProductService.CreateMasterProduct(model, description, brandId, cancellationToken);
        }

        public Task<DomainResult> UpdateAsync(Guid id, string model, string description, Guid brandId, ProductStatus productStatus, CancellationToken cancellationToken)
        {
            return _masterProductService.UpdateAsync(id, model, description, brandId, productStatus, cancellationToken);
        }

        public Task<List<MasterProduct>> GetManyAsync(CancellationToken cancellationToken)
        {
            return _masterProductService.GetManyAsync(cancellationToken);
        }

        public Task<DomainResult<MasterProduct>> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            return _masterProductService.GetOneAsync(id, cancellationToken);
        }

    }
}
