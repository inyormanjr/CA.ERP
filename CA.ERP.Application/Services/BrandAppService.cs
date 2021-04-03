using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.UnitOfWorkAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.Services
{
    public interface IBrandAppService : IAppService
    {
        Task<DomainResult<Guid>> CreateBrandAsync(string name, string description, CancellationToken cancellationToken);
        Task<DomainResult> UpdateAsync(Guid id, string name, string description, CancellationToken cancellationToken);
        Task<List<Brand>> GetManyAsync(CancellationToken cancellationToken);
        Task<DomainResult<Brand>> GetOneAsync(Guid id, CancellationToken cancellationToken);
        Task<DomainResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
    public class BrandAppService : IBrandAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandRepository _brandRepository;

        public BrandAppService(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }
        public async Task<DomainResult<Guid>> CreateBrandAsync(string name, string description, CancellationToken cancellationToken)
        {
            var result = Brand.Create(name, description);
            if (!result.IsSuccess)
            {
                return result.ConvertTo<Guid>();
            }

            var brand = result.Result;
            var id = await _brandRepository.AddAsync(brand, cancellationToken: cancellationToken);
            
            await _unitOfWork.CommitAsync();
            return DomainResult<Guid>.Success(id);

        }

        public Task<DomainResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Brand>> GetManyAsync(CancellationToken cancellationToken)
        {
            return _brandRepository.GetManyAsync(cancellationToken: cancellationToken);
        }

        public async Task<DomainResult<Brand>> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            Brand brand = await _brandRepository.GetByIdAsync(id, cancellationToken);
            if (brand == null)
            {
                return DomainResult<Brand>.Error(ErrorType.NotFound, BrandErrorCodes.InvalidName, "Brand not found");
            }
            return DomainResult<Brand>.Success(brand);
        }

        public async Task<DomainResult> UpdateAsync(Guid id, string name, string description, CancellationToken cancellationToken)
        {
            Brand brand = await _brandRepository.GetByIdAsync(id, cancellationToken);
            if (brand == null)
            {
                return DomainResult.Error(ErrorType.NotFound, BrandErrorCodes.InvalidName, "Brand not found");
            }
            brand.Update(name, description);
            await _brandRepository.UpdateAsync(id, brand, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }
    }
}
