using Core.Data.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfUserRepository : EfEntityRepository<User>, IUserDal
    {
        private readonly eReconciliationDb _context;
        public EfUserRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
