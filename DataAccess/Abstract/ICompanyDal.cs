﻿using Core.Data.Abstract;
using Domain.Concrete;

namespace DataAccess.Abstract
{
    public interface ICompanyDal:IEntityRepository<Company>
    {
        Task<bool> IsCompanyExists(Company company);
    }
}
