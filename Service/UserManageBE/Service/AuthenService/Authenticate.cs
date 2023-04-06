using Domain.Models;
using Infrastructure.Encrypt;
using Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenService
{
    /// <summary>
    ///   <para>Authenticate Service</para>
    ///   <para>
    ///     <br />
    ///   </para>
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// longpv 3/30/2023 created
    /// </Modified>
    public class Authenticate : IAuthenticate
    {
        private readonly IUserService _userService;
        private readonly IMD5Encrypt _md5;
        public Authenticate(IUserService userService, IMD5Encrypt md5)
        {
            _userService = userService;
            _md5 = md5;
        }
        /// <summary>Kiểm tra đăng nhập</summary>
        /// <param name="loginModel">The login model.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public User CheckLogin(LoginModel loginModel)
        {
            var users = _userService.GetAll();
            var pass = _md5.EncryptPassword(loginModel.Password);
            var user = users.Where(x => x.LoginName == loginModel.LoginName && x.Password == pass).FirstOrDefault();
            if (user != null) return user;
            else return null;
        }

    }
}
