using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthenService
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// longpv 3/30/2023 created
    /// </Modified>
    public interface IAuthenticate
    {
        User CheckLogin(LoginModel loginModel);
    }
}
