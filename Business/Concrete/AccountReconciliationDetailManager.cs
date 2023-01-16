using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class AccountReconciliationDetailManager : IAccountReconciliationDetailService
    {
        private readonly IAccountReconciliationDetailDal _accountReconciliationDetailDal;

        public AccountReconciliationDetailManager(IAccountReconciliationDetailDal accountReconciliationDetailDal)
        {
            _accountReconciliationDetailDal = accountReconciliationDetailDal;
        }

        public async Task<IResult> AddAsync(AccountReconciliationDetail accountReconciliationDetail)
        {
            bool isSucceded = await _accountReconciliationDetailDal.AddAsync(accountReconciliationDetail);
            if (isSucceded)
            {
                int save = await _accountReconciliationDetailDal.SaveChangesAsync();
                if (save > -1)
                    return new Result(ResultStatus.Success, "Ekleme işlemi başarılı");
                return new Result(ResultStatus.Failed, "Veritabanına kayıt işlemi esnasında bir hata meydana geldi");

            }
            return new Result(ResultStatus.Failed, "Bir hata meydana geldi");

        }

        public async Task<IResult> DeleteAsync(int id)
        {
            bool isDeleted = await _accountReconciliationDetailDal.DeleteAsync(id);
            if (isDeleted)
            {
                int save = await _accountReconciliationDetailDal.SaveChangesAsync();
                if (save > -1)
                {
                    return new Result(ResultStatus.Success, "Silme işlemi başarılı");
                }
                return new Result(ResultStatus.Failed, "Veritabanına kayıt işlemi esnasında bir hata meydana geldi");
            }
            return new Result(ResultStatus.Failed,"Silme işlemi esnasında bir hata meydana geldi");

        }

        public async Task<IDataResult<List<AccountReconciliationDetail>>> GetAccountReconciliationDetailAsync()
        {
            var datas = await _accountReconciliationDetailDal.GetAll(false).ToListAsync();
            if(datas.Count > -1)
            {
                return new DataResult<List<AccountReconciliationDetail>>(datas,ResultStatus.Success);
            }
            return new DataResult<List<AccountReconciliationDetail>>(null, ResultStatus.Failed,"Listeleme işlemi esnasında bir hata meydana geldi");
        }

        public async Task<IDataResult<AccountReconciliationDetail>> GetAccountReconciliationDetailById(int id)
        {
            var data = await _accountReconciliationDetailDal.GetSingle(ar => ar.Id == id);
            return new DataResult<AccountReconciliationDetail>(data,ResultStatus.Success);
        }

        public async Task<IResult> UpdateAsync(AccountReconciliationDetail accountReconciliationDetail)
        {
           bool isUpdated = _accountReconciliationDetailDal.Update(accountReconciliationDetail);
            if (isUpdated)
            {
                await _accountReconciliationDetailDal.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Güncelleme işlemi başarılı");
            }
                return new Result(ResultStatus.Failed, "Güncelleme işlemi esnasında bir hata meydana geldi");

        }
    }
}
