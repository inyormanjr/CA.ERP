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
        private readonly IRepository<T> _repository;
        private readonly IValidator<T> _validator;

        public ServiceBase(IRepository<T> repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public virtual async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            OneOf<Success, NotFound> ret;

            var deleteOption = await _repository.DeleteAsync(id, cancellationToken: cancellationToken);
            ret = deleteOption.Match<OneOf<Success, NotFound>>(
                f0: success => success,
                f1: none => default(NotFound)
                );
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
                var supplierOption = await _repository.UpdateAsync(id, model, cancellationToken: cancellationToken);
                ret = supplierOption.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                    f0: supplierId => supplierId,
                    f1: none => default(NotFound)
                    );

            }
            return ret;
        }

        public virtual async Task<List<T>> GetManyAsync(CancellationToken cancellationToken = default)
        {
            List<T> suppliers = await _repository.GetManyAsync(cancellationToken: cancellationToken);
            return suppliers;
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
