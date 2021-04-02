using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Core.DomainResullts
{
    public class DomainResult
    {

        public bool IsSuccess { get; private set; }
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        protected DomainResult(bool isSuccess, string errorCode, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static DomainResult Success()
        {
            return new DomainResult(true, null, null);
        }

        public static DomainResult Error(string errorCode, string errorMessage)
        {
            return new DomainResult(false, errorCode, errorMessage);
        }
    }

    public class DomainResult<T> : DomainResult
    {
        public T Result { get; private set; }

        protected DomainResult(bool isSuccess, T result, string errorCode, string errorMessage) : base(isSuccess, errorCode, errorMessage)
        {
            Result = result;
        }

        /// <summary>
        /// User to convert domain results with data
        /// </summary>
        /// <typeparam name="T2">The destination type</typeparam>
        /// <returns></returns>
        public DomainResult<T2> ConvertTo<T2>()
        {
            if (IsSuccess)
            {
                throw new Exception("Unable to convert success result");
            }
            return new DomainResult<T2>(false, default, ErrorCode, ErrorMessage);
        }

        public static DomainResult<T> Success(T result)
        {
            return new DomainResult<T>(true, result, null, null);
        }

        public static DomainResult<T> Error(string errorCode, string errorMessage)
        {
            return new DomainResult<T>(false, default, errorCode, errorMessage);
        }

    }
}
