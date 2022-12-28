using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfAccountReconciliationDetailRepository : EfEntityRepository<AccountReconciliationDetail>, IAccountReconciliationDetailDal
    {
        private readonly eReconciliationDb _context;
        public EfAccountReconciliationDetailRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
