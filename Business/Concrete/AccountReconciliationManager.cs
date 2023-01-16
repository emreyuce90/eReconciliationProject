using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using a = Domain.Concrete;

namespace Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal _accountReconciliationDal;

        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal)
        {
            _accountReconciliationDal = accountReconciliationDal;
        }

        public async Task<IResult> AddAsync(a.AccountReconciliation accountReconciliation)
        {
            //validasyon yap

            bool isSucceded = await _accountReconciliationDal.AddAsync(accountReconciliation);
            if (isSucceded)
            {
                //Ekleme operasyonunu başarılı ise 
                await _accountReconciliationDal.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Ekleme işlemi başarılı");

            }
            return new Result(ResultStatus.Failed, "Ekleme işlemi başarısız");

        }

        public async Task<IDataResult<List<a.AccountReconciliation>>> GetAllAsync()
        {
            var datas = await _accountReconciliationDal.GetAll().ToListAsync();
            if(datas.Count > -1)
            {
                return new DataResult<List<a.AccountReconciliation>>(datas, ResultStatus.Success);
            }
            return new DataResult<List<a.AccountReconciliation>>(null, ResultStatus.Failed,"Listede bir hata meydana geldi");

        }

        public async Task<IDataResult<a.AccountReconciliation>> GetAsync(int id)
        {
            var data = await _accountReconciliationDal.GetSingle(a => a.Id == id);
            return new DataResult<a.AccountReconciliation>(data, ResultStatus.Success);
        }

        public async Task<IResult> UpdateAsync(a.AccountReconciliation account)
        {
            _accountReconciliationDal.Update(account);
            await _accountReconciliationDal.SaveChangesAsync();
            return new Result(ResultStatus.Success);
        }
    }
}
