using Core.Data.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfMailParameterRepository : EfEntityRepository<MailParameter>, IMailParameterDal
    {
        private readonly eReconciliationDb _context;
        public EfMailParameterRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
