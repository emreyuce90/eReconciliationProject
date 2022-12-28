using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCurrencyRepository : EfEntityRepository<Currency>, ICurrencyDal
    {
        private readonly eReconciliationDb _context;
        public EfCurrencyRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
