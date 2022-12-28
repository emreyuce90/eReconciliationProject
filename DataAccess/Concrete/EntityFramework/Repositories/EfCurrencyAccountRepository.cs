using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCurrencyAccountRepository : EfEntityRepository<CurrencyAccount>, ICurrencyAccountDal
    {
        private readonly eReconciliationDb _context;
        public EfCurrencyAccountRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
