﻿using Core.Entities.Concrete;
using Core.Utilities.JWT;
using Core.Utilities.Result.Abstract;
using Domain.Concrete.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        /// <summary>
        /// Userlogindto alır ve geriye user döner
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        Task<IDataResult<User>> Login(UserLoginDto userLoginDto);
        /// <summary>
        /// Bir user alır ve bu user a bir token döner
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataResult<AccessToken> CreateToken(User user);
        /// <summary>
        /// Kullanıcıyı veritabanına kayıt eder
        /// </summary>
        /// <param name="userRegisterDto"></param>
        /// <returns></returns>
        IDataResult<User> Register(UserRegisterDto userRegisterDto);
        /// <summary>
        /// Kullanıcı var mı yok mu kontrol eder
        /// </summary>
        /// <param name="email">Parametre olarak kullanıcının e posta adresini alır</param>
        /// <returns></returns>
        IResult CheckUserExist(string email);
    }
}
