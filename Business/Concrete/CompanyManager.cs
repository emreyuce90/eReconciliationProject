using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task AddAsync(Company company)
        {
            bool isAdded=await _companyDal.AddAsync(company);
            if (isAdded)
            {
                await _companyDal.SaveChangesAsync();
            }

        }

        public Task DeleteAsync(Company company)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<Company>>> GetAllAsync()
        {
            var allCompanies = await _companyDal.GetAll().ToListAsync();
            return new DataResult<List<Company>>(allCompanies,ResultStatus.Success, "Veritabanı sorgulama işlemi başarılı");
        }

        public Task<Company> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Company company)
        {
            throw new NotImplementedException();
        }


    }
}
