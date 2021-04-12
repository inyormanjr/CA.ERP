using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Core.DomainResullts
{
    public enum ErrorType
    {
        Success,
        Error,
        Forbidden,
        NotFound
    }
    public class DomainResult
    {

        public bool IsSuccess { get; private set; }
        public ErrorType ErrorType { get; private set; }
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        protected DomainResult(bool isSuccess, ErrorType errorType, string errorCode, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorType = errorType;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static DomainResult Success()
        {
            return new DomainResult(true, ErrorType.Success, null, null);
        }

        public static DomainResult Error(string errorCode, string errorMessage)
        {
            return new DomainResult(false, ErrorType.Error, errorCode, errorMessage);
        }

        public static DomainResult Error(ErrorType errorType, string errorCode, string errorMessage)
        {
            return new DomainResult(false, errorType, errorCode, errorMessage);
        }
    }

    public class DomainResult<T> : DomainResult
    {
        public T Result { get; private set; }

        protected DomainResult(bool isSuccess, T result, ErrorType errorType, string errorCode, string errorMessage) : base(isSuccess, errorType, errorCode, errorMessage)
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
            return new DomainResult<T2>(false, default, ErrorType, ErrorCode, ErrorMessage);
        }

        public static DomainResult<T> Success(T result)
        {
            return new DomainResult<T>(true, result, ErrorType.Success, null, null);
        }

        public static DomainResult<T> Error(string errorCode, string errorMessage)
        {
            return new DomainResult<T>(false, default, ErrorType.Error, errorCode, errorMessage);
        }

        public static DomainResult<T> Error(ErrorType errorType,string errorCode, string errorMessage)
        {
            return new DomainResult<T>(false, default, errorType, errorCode, errorMessage);
        }


    }
}
