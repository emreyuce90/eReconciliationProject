using Business.Abstract;
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

        public async Task<List<Company>> GetAllAsync()
        {
            var companies =_companyDal.GetAll(false);
            return await companies.ToListAsync();
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
