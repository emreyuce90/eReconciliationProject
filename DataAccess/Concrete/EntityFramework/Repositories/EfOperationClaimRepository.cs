using Core.Data.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfOperationClaimRepository : EfEntityRepository<OperationClaim>, IOperationClaimDal
    {
        private readonly eReconciliationDb _context;
        public EfOperationClaimRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
