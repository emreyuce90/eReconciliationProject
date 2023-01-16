using Business.Abstract;
using Business.CrossCuttingConcerns.ValidationRules;
using Core.CrossCuttingCoıncerns.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using r = Core.Utilities.Result.Abstract;
namespace Business.Concrete
{
    public class CurrencyAccountManager : ICurrencyAccountService
    {
        private readonly ICurrencyAccountDal _currencyAccountDal;

        public CurrencyAccountManager(ICurrencyAccountDal currencyAccountDal)
        {
            _currencyAccountDal = currencyAccountDal;
        }

        public async Task<r.IResult> AddAsync(CurrencyAccount currencyAccount)
        {
            ValidationHelper.ValidateObject(new CurrencyAccountValidator(), currencyAccount);
            await _currencyAccountDal.AddAsync(currencyAccount);
            int save = await _currencyAccountDal.SaveChangesAsync();
            if (save > -1)
            {
                return new Result(ResultStatus.Success, "Ekleme işlemi başarılı");

            }
            return new Result(ResultStatus.Failed, "Ekleme işlemi başarısız oldu");

        }

        public async Task<r.IResult> AddToExcel(string filePath, int companyId)
        {
            //Hata vermemesi için yaptığımız ayar
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //okuma için stream oluşturuyoruz
            using (var stream = System.IO.File.Open(filePath, FileMode.Open,FileAccess.Read))
            {
                //create reader oluşturup içerisine stream i veriyoruz
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //reader okuyana kadar devam etsin
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);
                        string name = reader.GetString(1);
                        string address = reader.GetString(2);
                        string taxdepartment = reader.GetString(3);
                        string taxCode = reader.GetString(4);
                        string tc = reader.GetString(5);
                        string mail = reader.GetString(6);
                        string authorized = reader.GetString(7);


                        if (code != "Cari Kodu")
                        {
                            CurrencyAccount currencyAccount = new()
                            {
                                Name = name,
                                Address = address,
                                TaxDepartment = taxdepartment,
                                TaxIdNumber = taxCode,
                                AddedAt = DateTime.Now,
                                AuthorizedPerson = authorized,
                                Code = code,
                                CompanyId = companyId,
                                EMail = mail,
                                IdentityNumber = tc,
                                IsActive = true

                            };
                            ValidationHelper.ValidateObject(new CurrencyAccountValidator(), currencyAccount);
                            await _currencyAccountDal.AddAsync(currencyAccount);
                            await _currencyAccountDal.SaveChangesAsync();
                           

                        }

                    }
                }
                return new Result(ResultStatus.Success, "Excel dosyası başarıyla veritabanına kaydedildi");
            }
            return new Result(ResultStatus.Failed, "Bir hata meydana geldi");
        }

        public async Task<r.IResult> DeleteAsync(int id)
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

        public async Task<IDataResult<CurrencyAccount>> GetCurrencyAccountByCodeandCompanyId(int companyId, string code)
        {
            var ca =await _currencyAccountDal.GetSingle(ca=>ca.CompanyId== companyId && ca.Code == code);
            return new DataResult<CurrencyAccount>(ca, ResultStatus.Success);
        }

        public async Task<IDataResult<CurrencyAccount>> GetSingle(int id)
        {
            var account = await _currencyAccountDal.GetSingle(ca => ca.Id == id, false);
            return new DataResult<CurrencyAccount>(account, ResultStatus.Success);
        }

        public async Task<r.IResult> UpdateAsync(CurrencyAccount currencyAccount)
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
