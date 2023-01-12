using Core.Utilities.Result.Abstract;
using Domain.Concrete;

namespace Business.Abstract
{
    public interface ICurrencyAccountService
    {
        Task<IDataResult<List<CurrencyAccount>>> GetAllAsync();
        Task<IDataResult<CurrencyAccount>> GetSingle(int id);
        Task<IResult> AddAsync(CurrencyAccount currencyAccount);
        Task<IResult> UpdateAsync(CurrencyAccount currencyAccount);
        Task<IResult> DeleteAsync(int id);
    }
}
