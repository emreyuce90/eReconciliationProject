using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfBaBsReconciliationDetailRepository : EfEntityRepository<BaBsReconciliationDetail>, IBaBsReconciliationDetailDal
    {
        private readonly eReconciliationDb _context;
        public EfBaBsReconciliationDetailRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
