﻿using Business.Abstract;
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
        private readonly ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IResult> CheckUserExist(string email)
        {
           User user = await _userService.GetUserByEMail(email);
            if (user != null)
            {
                return new Result(ResultStatus.Failed, "Sistemde zaten böyle bir kullanıcı kayıtlı");
            }
            return new Result(ResultStatus.Success);

        }

        public async Task<IDataResult<AccessToken>> CreateToken(User user, int companyId)
        {
            var claimList = await _userService.GetOperationClaims(companyId, user);
            var accessToken = _tokenHelper.CreateToken(user, claimList, companyId);
            return new DataResult<AccessToken>(accessToken, ResultStatus.Success);
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

        public async Task<IDataResult<User>> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            User user = new()
            {
                AddedAt = DateTime.Now,
                EMail = userRegisterDto.EMail,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                Name = userRegisterDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _userService.AddAsync(user);
            return new DataResult<User>(user, ResultStatus.Success);
        }
    }
}
