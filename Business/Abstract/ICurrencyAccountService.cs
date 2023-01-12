using Core.Utilities.Result.Abstract;
using Domain.Concrete;
using Microsoft.AspNetCore.Http;
using r = Core.Utilities.Result.Abstract;

namespace Business.Abstract
{
    public interface ICurrencyAccountService
    {
        Task<IDataResult<List<CurrencyAccount>>> GetAllAsync();
        Task<IDataResult<CurrencyAccount>> GetSingle(int id);
        Task<r.IResult> AddAsync(CurrencyAccount currencyAccount);
        Task<r.IResult> UpdateAsync(CurrencyAccount currencyAccount);
        Task<r.IResult> DeleteAsync(int id);
        Task<r.IResult> AddToExcel(string filePath, int companyId);
    }
}
