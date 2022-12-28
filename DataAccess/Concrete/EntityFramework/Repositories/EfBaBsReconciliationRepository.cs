﻿using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfBaBsReconciliationRepository : EfEntityRepository<BaBsReconciliation>, IBaBsReconciliationDal
    {
        private readonly eReconciliationDb _context;
        public EfBaBsReconciliationRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}