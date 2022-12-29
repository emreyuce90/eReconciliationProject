using Core.Utilities.Result.ComplexTypes;

namespace Core.Utilities.Result.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get;}
        public string Message { get;}
        public Exception Exception { get;}
    }
}
