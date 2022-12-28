using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCompanyRepository : EfEntityRepository<Company>, ICompanyDal
    {
        private readonly eReconciliationDb _context;
        public EfCompanyRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
