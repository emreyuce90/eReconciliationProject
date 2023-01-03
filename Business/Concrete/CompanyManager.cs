using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public async Task<IResult> AddAsync(Company company)
        {
            bool isAdded = await _companyDal.AddAsync(company);
            if (isAdded)
            {
                await _companyDal.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{company} adlı firma başarıyla eklendi");
            }
            return new Result(ResultStatus.Failed, $"{company.Name} adlı firma eklenemedi");

        }

        public async Task<IResult> DeleteAsync(int id)
        {
            Company c = await _companyDal.GetSingle(c => c.Id == id, false);
            bool isDeleted = _companyDal.Delete(c);
            if (isDeleted)
            {
                await _companyDal.SaveChangesAsync();
                return new Result(ResultStatus.Success, $"{c.Name} adlı firmaya ait tüm veriler başarıyla silindi");
            }
            return new Result(ResultStatus.Failed, "Bir hata meydana geldi");

        }

        public async Task<IDataResult<List<Company>>> GetAllAsync()
        {
            var allCompanies = await _companyDal.GetAll().ToListAsync();
            if (allCompanies.Count > -1)
            {
                return new DataResult<List<Company>>(allCompanies, ResultStatus.Success, "Veritabanı sorgulama başarılı");

            }
            else
            {
                return new DataResult<List<Company>>(allCompanies, ResultStatus.Failed, "Veritabanı sorgulama başarısız");

            }
        }

        public Task<IDataResult<Company>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> IsCompanyExists(Company company)
        {
           bool isExists =await _companyDal.IsCompanyExists(company);
            if (isExists)
            {
                return new Result(ResultStatus.Success);
            }
            return new Result(ResultStatus.Failed);

        }

        public async Task<IResult> UpdateAsync(Company company)
        {
            bool isUpdated = _companyDal.Update(company);
            if (isUpdated)
            {
                await _companyDal.SaveChangesAsync();

                return new Result(ResultStatus.Success,$"{company.Name} adlı firma bilgileri güncellendi");
            }
            return new Result(ResultStatus.Failed, $"Bir hata meydana geldi {company.Name} adlı firma bilgiler güncellenemedi");

        }
    }
}
