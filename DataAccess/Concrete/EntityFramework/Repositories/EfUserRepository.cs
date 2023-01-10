using Core.Data.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;
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

        public async Task<UserCompany> GetUserCompanyByUserIdAsync(int userId)
        {
            UserCompany? uc = await _context.UserCompanies.FirstOrDefaultAsync(c => c.UserId == userId);
            if (uc != null)
            {
                return uc;
            }
            throw new Exception(message: "Bir hata meydana geldi");
        }

        public async Task<List<OperationClaim>> ListClaims(int companyId, User user)
        {
            //kullanıcı id ve company id sine göre claimleri listele
            //claimler tablosu ile useroperationclaims i birleştirmemiz gerekir
            var claims = await _context.UserOperationClaims.Join(_context.OperationClaims, uo => uo.OperationClaimId, oc => oc.Id, (userOperation, claim) => new
            {
                Id = claim.Id,
                Name = claim.Name,
                CompanyId = userOperation.CompanyId,
                UserId = userOperation.UserId
            }).Where(s => s.CompanyId == companyId && s.UserId == user.Id).Select(x => new OperationClaim
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return claims;
        }
    }
}
