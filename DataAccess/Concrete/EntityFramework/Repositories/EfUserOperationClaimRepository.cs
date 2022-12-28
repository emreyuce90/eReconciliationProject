using Core.Data.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfUserOperationClaimRepository : EfEntityRepository<UserOperationClaim>, IUserOperationClaimDal
    {
        private readonly eReconciliationDb _context;
        public EfUserOperationClaimRepository(eReconciliationDb context) : base(context)
        {
            _context = context;
        }
    }
}
