using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfUserCompanyRepository : EfEntityRepository<UserCompany>, IUserCompanyDal
    {
        private readonly eReconciliationDb _context;
        public EfUserCompanyRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
