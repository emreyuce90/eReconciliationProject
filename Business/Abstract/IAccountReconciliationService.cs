using Core.Utilities.Result.Abstract;
using Domain.Concrete;

namespace Business.Abstract
{
    public interface IAccountReconciliationService
    {
        Task<IResult> AddAsync(AccountReconciliation accountReconciliation);

        Task<IResult> UpdateAsync(AccountReconciliation account);

        Task<IDataResult<List<AccountReconciliation>>> GetAllAsync();

        Task<IDataResult<AccountReconciliation>> GetAsync(int id);

    }
}
