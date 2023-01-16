using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;
using a = Domain.Concrete;

namespace Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal _accountReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;
        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal, ICurrencyAccountService currencyAccountService)
        {
            _accountReconciliationDal = accountReconciliationDal;
            _currencyAccountService = currencyAccountService;
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

        public async Task<IResult> AddToExcel(string path, int companyId)
        {

            //Hata vermemesi için yaptığımız ayar
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);
                        if (code != "Cari Kodu" && code != null)
                        {
                            DateTime startDate = reader.GetDateTime(1);
                            DateTime endDate = reader.GetDateTime(2);
                            double currencyId = reader.GetDouble(3);
                            double dept = reader.GetDouble(4);
                            double credit = reader.GetDouble(5);


                            var currencyAccountId = await _currencyAccountService.GetCurrencyAccountByCodeandCompanyId(companyId, code);
                            var ar = new AccountReconciliation
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId.Data.Id,
                                CurrencyCredit = Convert.ToDecimal(credit),
                                CurrencyDebit = Convert.ToDecimal(dept),
                                StartingDate = startDate,
                                EndingDate = endDate,
                                CurrencyId=Convert.ToInt16(currencyId)
                            };

                            await _accountReconciliationDal.AddAsync(ar);
                            await _accountReconciliationDal.SaveChangesAsync();
                        }

                    }
                    return new Result(ResultStatus.Success, "Veritabanı kaydı başarılı");
                }
            }
        }

        public async Task<IDataResult<List<a.AccountReconciliation>>> GetAllAsync()
        {
            var datas = await _accountReconciliationDal.GetAll().ToListAsync();
            if (datas.Count > -1)
            {
                return new DataResult<List<a.AccountReconciliation>>(datas, ResultStatus.Success);
            }
            return new DataResult<List<a.AccountReconciliation>>(null, ResultStatus.Failed, "Listede bir hata meydana geldi");

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
