using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using ExcelDataReader;
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

        public async Task<IResult> AddToExcel(string path, int reconciliationId)
        {
            //Hata vermemesi için
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //pathi oku
            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);
                        if (description != "Açıklama" && description != null)
                        {
                            double currencyId = reader.GetDouble(2);
                            double debit = reader.GetDouble(3);
                            double credit = reader.GetDouble(4);

                            DateTime date = reader.GetDateTime(0);
                            //nesneyi oluştur ve doldur
                            AccountReconciliationDetail detail = new()
                            {
                                AccountReconciliationsId = reconciliationId,
                                CurrencyCredit = Convert.ToDecimal(credit),
                                CurrencyDebit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt16(currencyId),
                                Date = date,
                                Description = description
                            };
                            await _accountReconciliationDetailDal.AddAsync(detail);
                            await _accountReconciliationDetailDal.SaveChangesAsync();
                        }
                    }                  
                }
            }
            File.Delete(path);
            return new Result(ResultStatus.Success, "Excel veritabanına kaydedildi");
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
            return new Result(ResultStatus.Failed, "Silme işlemi esnasında bir hata meydana geldi");

        }

        public async Task<IDataResult<List<AccountReconciliationDetail>>> GetAccountReconciliationDetailAsync()
        {
            var datas = await _accountReconciliationDetailDal.GetAll(false).ToListAsync();
            if (datas.Count > -1)
            {
                return new DataResult<List<AccountReconciliationDetail>>(datas, ResultStatus.Success);
            }
            return new DataResult<List<AccountReconciliationDetail>>(null, ResultStatus.Failed, "Listeleme işlemi esnasında bir hata meydana geldi");
        }

        public async Task<IDataResult<AccountReconciliationDetail>> GetAccountReconciliationDetailById(int id)
        {
            var data = await _accountReconciliationDetailDal.GetSingle(ar => ar.Id == id);
            return new DataResult<AccountReconciliationDetail>(data, ResultStatus.Success);
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
