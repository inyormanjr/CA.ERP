using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IUserHelper _userHelper;

        public MasterProductAppService(MasterProductService masterProductService, IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository, IUserHelper userHelper)
        {
            _masterProductService = masterProductService;
            _unitOfWork = unitOfWork;
            _masterProductRepository = masterProductRepository;
            _userHelper = userHelper;
        }
        public async Task<DomainResult<Guid>> CreateMasterProduct(string model, string description, Guid brandId, CancellationToken cancellationToken = default)
        {
            var result = MasterProduct.Create(model, description, brandId);
            if (result.IsSuccess)
            {
                MasterProduct masterProduct = result.Result;
                masterProduct.CreatedBy = _userHelper.GetCurrentUserId();
                masterProduct.UpdatedBy = _userHelper.GetCurrentUserId();
                Guid id = await _masterProductRepository.AddAsync(masterProduct, cancellationToken);
                await _unitOfWork.CommitAsync();
                return DomainResult<Guid>.Success(id);
            }
            else
            {
                return result.ConvertTo<Guid>();
            }
        }

        public async Task<DomainResult> UpdateAsync(Guid id, string model, string description, Guid brandId, ProductStatus productStatus, CancellationToken cancellationToken)
        {
            var masterProduct = await _masterProductRepository.GetByIdAsync(id, cancellationToken);
            if (masterProduct == null)
            {
                return DomainResult.Error(MasterProductErrorCodes.MasterProductNotFound, "Master product was not found while updating");
            }

            masterProduct.Update(model, description, brandId, productStatus);

            await _masterProductRepository.UpdateAsync(id, masterProduct);
            return DomainResult.Success();
        }

        public Task<List<MasterProduct>> GetManyAsync(CancellationToken cancellationToken)
        {
            return _masterProductRepository.GetManyAsync(cancellationToken: cancellationToken);
        }

        public Task<DomainResult<MasterProduct>> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            return _masterProductService.GetOneAsync(id, cancellationToken);
        }

    }
}
