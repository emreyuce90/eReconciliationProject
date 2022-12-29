using Core.Data.Abstract;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> ListClaims(int companyId, User user);
    }
}
