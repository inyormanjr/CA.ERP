using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Base
{
    public abstract class ServiceBase
    { 
    }
    public abstract class ServiceBase<T> : ServiceBase where T: ModelBase 
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T> _repository;
        protected readonly IValidator<T> _validator;
        protected readonly IUserHelper _userHelper;

        public ServiceBase(IUnitOfWork unitOfWork, IRepository<T> repository, IValidator<T> validator, IUserHelper userHelper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _validator = validator;
            _userHelper = userHelper;
        }
        public virtual async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            OneOf<Success, NotFound> ret;

            var deleteOption = await _repository.DeleteAsync(id, cancellationToken: cancellationToken);
            ret = deleteOption.Match<OneOf<Success, NotFound>>(
                f0: success => success,
                f1: none => default(NotFound)
                );

            await _unitOfWork.CommitAsync();
            return ret;
        }

        public virtual async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateAsync(Guid id, T model, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>, NotFound> ret;

            //validation
            var validationResult = _validator.Validate(model);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                model.UpdatedBy = _userHelper.GetCurrentUserId();
                var supplierOption = await _repository.UpdateAsync(id, model, cancellationToken: cancellationToken);
                ret = supplierOption.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                    f0: supplierId => supplierId,
                    f1: none => default(NotFound)
                    );

            }
            await _unitOfWork.CommitAsync();
            return ret;
        }

        public virtual async Task<List<T>> GetManyAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetManyAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<OneOf<T, NotFound>> GetOneAsync(Guid id, CancellationToken cancellationToken = default)
        {
            OneOf<T, None> getOption = await _repository.GetByIdAsync(id, cancellationToken: cancellationToken);
            return getOption.Match<OneOf<T, NotFound>>(
                f0: model => {
                    return model;
                },
                f1: none => default(NotFound)
                );
        }
    }

}
