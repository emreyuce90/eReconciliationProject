using Core.Data.Abstract;
using Core.Entities.Concrete;
using Domain.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> ListClaims(int companyId, User user);
        Task<UserCompany> GetUserCompanyByUserIdAsync(int userId);
    }
}
