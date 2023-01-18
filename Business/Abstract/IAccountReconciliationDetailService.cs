using Core.Utilities.Result.Abstract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAccountReconciliationDetailService
    {
        Task<IDataResult<List<AccountReconciliationDetail>>> GetAccountReconciliationDetailAsync();
        Task<IDataResult<AccountReconciliationDetail>> GetAccountReconciliationDetailById(int id);
        Task<IResult> AddAsync(AccountReconciliationDetail accountReconciliationDetail);
        Task<IResult> UpdateAsync(AccountReconciliationDetail accountReconciliationDetail);
        Task<IResult> DeleteAsync(int id);
        Task<IResult> AddToExcel(string path,int reconciliationId);
    }
}
