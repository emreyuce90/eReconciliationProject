using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.JWT;
using Core.Utilities.Result.Abstract;
using Domain.Concrete.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        public IResult CheckUserExist(string email)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AccessToken> CreateToken(User user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
