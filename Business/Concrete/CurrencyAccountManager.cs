using Business.Abstract;
using Business.CrossCuttingConcerns.ValidationRules;
using Core.CrossCuttingCoıncerns.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class CurrencyAccountManager : ICurrencyAccountService
    {
        private readonly ICurrencyAccountDal _currencyAccountDal;

        public CurrencyAccountManager(ICurrencyAccountDal currencyAccountDal)
        {
            _currencyAccountDal = currencyAccountDal;
        }

        public async Task<IResult> AddAsync(CurrencyAccount currencyAccount)
        {
            ValidationHelper.ValidateObject(new CurrencyAccountValidator(),currencyAccount);
            await _currencyAccountDal.AddAsync(currencyAccount);
            int save = await _currencyAccountDal.SaveChangesAsync();
            if (save > -1)
            {
                return new Result(ResultStatus.Success, "Ekleme işlemi başarılı");

            }
            return new Result(ResultStatus.Failed, "Ekleme işlemi başarısız oldu");

        }

        public async Task<IResult> DeleteAsync(int id)
        {
            await _currencyAccountDal.DeleteAsync(id);
            int save = await _currencyAccountDal.SaveChangesAsync();
            if (save > -1)
                return new Result(ResultStatus.Success, "Silme işlemi başarılı");
            return new Result(ResultStatus.Failed, "Silme işlemi başarısız");
        }

        public async Task<IDataResult<List<CurrencyAccount>>> GetAllAsync()
        {
            var list = _currencyAccountDal.GetAll(false);
            return new DataResult<List<CurrencyAccount>>(await list.ToListAsync(), ResultStatus.Success);
        }

        public async Task<IDataResult<CurrencyAccount>> GetSingle(int id)
        {
            var account = await _currencyAccountDal.GetSingle(ca => ca.Id == id, false);
            return new DataResult<CurrencyAccount>(account, ResultStatus.Success);
        }

        public async Task<IResult> UpdateAsync(CurrencyAccount currencyAccount)
        {
            ValidationHelper.ValidateObject(new CurrencyAccountValidator(), currencyAccount);
            _currencyAccountDal.Update(currencyAccount);
            int save = await _currencyAccountDal.SaveChangesAsync();
            if (save > -1)
            {
                return new Result(ResultStatus.Success, "Güncelleme başarılı");
            }
            return new Result(ResultStatus.Failed, "Güncelleme işlemi başarısız");
        }
    }
}
