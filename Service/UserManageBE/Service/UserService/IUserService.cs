using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// longpv 3/30/2023 created
    /// </Modified>
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        User InsertUser(User user);
        User UpdateUser(User user);
        User DeleteUser(int id);
        IEnumerable<User> GetPaggingAndSearch(SearchModel model);

    }
}
