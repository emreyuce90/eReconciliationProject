﻿using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCompanyRepository : EfEntityRepository<Company>, ICompanyDal
    {
        private readonly eReconciliationDb _context;
        public EfCompanyRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsCompanyExists(Company company)
        {
            Company? c = await _context.Companies.FirstOrDefaultAsync(c => c.Id == company.Id && c.Name == c.Name && c.TaxIdNumber == c.TaxIdNumber && c.IdentityNumber == c.IdentityNumber);

            if (c != null)
            {
                return false;
            }
            return true;
        }
    }
}
