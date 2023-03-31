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
        ResponseUser GetPagging(int pageIndex,int pageSize);
        IEnumerable<User> GetAll();
        User GetUser(int id);
        ResponseService<User> InsertUser(User user);
        ResponseService<User> UpdateUser(User user);
        ResponseService<User> DeleteUser(int id);
        ResponseUser GetUserSearch(SearchModel model);

    }
}
