using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
            await _userDal.SaveChangesAsync();
        }

        public async Task<List<OperationClaim>> GetOperationClaims(int companyId, User user)
        {
            return await _userDal.ListClaims(companyId, user);
        }

        public async Task<User> GetUserByEMail(string email)
        {
            return await _userDal.GetSingle(u => u.EMail == email);
        }
    }
}
