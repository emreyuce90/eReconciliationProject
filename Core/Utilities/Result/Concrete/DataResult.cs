using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(T data, ResultStatus resultStatus)
        {
            Data = data;
            ResultStatus= resultStatus;
        }

        public DataResult(T data, ResultStatus resultStatus,string message)
        {
            Message = message;
            ResultStatus= resultStatus;
            Data = data;
        }
        public DataResult(T data, ResultStatus resultStatus, string message, Exception exception)
        {
            Message = message;
            ResultStatus = resultStatus;
            Exception = exception;
        }

        public T Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
