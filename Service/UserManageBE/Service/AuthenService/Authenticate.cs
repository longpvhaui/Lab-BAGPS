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
    public class Authenticate : IAuthenticate
    {
        private readonly IUserService _userService;
        private readonly IMD5Encrypt _md5;
        public Authenticate(IUserService userService, IMD5Encrypt md5)
        {
            _userService = userService;
            _md5 = md5;
        }
        public User CheckLogin(LoginModel loginModel)
        {
            var users =  _userService.GetUsers();
            var pass = _md5.EncryptPassword(loginModel.Password);
            var user = users.Where(x => x.LoginName == loginModel.LoginName && x.Password == pass).FirstOrDefault();
            if (user != null) return user;
            else return null;
        }

    }
}
