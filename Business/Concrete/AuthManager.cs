using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using Domain.Concrete.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public IResult CheckUserExist(string email)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AccessToken> CreateToken(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<User>> Login(UserLoginDto userLoginDto)
        {
            //check user
            var user = await _userService.GetUserByEMail(userLoginDto.EMail);
            if (user == null)
            {
                return new DataResult<User>(null, ResultStatus.Failed, "Böyle bir kullanıcı bulunamadı");
            }
            //check password
            bool isTrue = HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (isTrue)
            {
                return new DataResult<User>(user, ResultStatus.Success);
            }

            return new DataResult<User>(null, ResultStatus.Failed, "Kullanıcı adı veya şifre hatalı");


        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
