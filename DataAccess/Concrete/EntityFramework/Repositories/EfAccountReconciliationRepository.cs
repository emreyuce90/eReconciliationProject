using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfAccountReconciliationRepository : EfEntityRepository<AccountReconciliation>, IAccountReconciliationDal
    {
        private readonly eReconciliationDb _context;
        public EfAccountReconciliationRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
