using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Domain.Concrete;

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

        public async Task<User> GetUserByConfirmValue(string confirmValue)
        {
            User u =await _userDal.GetSingle(u=>u.MailConfirmValue == confirmValue);
            return u;
        }

        public async Task<User> GetUserByEMail(string email)
        {
            return await _userDal.GetSingle(u => u.EMail == email);
        }

        public async Task<User> GetUserByUserId(int id)
        {
           User? user= await _userDal.GetSingle(u => u.Id == id);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<UserCompany> GetUserCompanyByUserIdAsync(int userId)
        {
            return await _userDal.GetUserCompanyByUserIdAsync(userId);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _userDal.Update(user);
            await _userDal.SaveChangesAsync();
            return true;
        }
    }
}
