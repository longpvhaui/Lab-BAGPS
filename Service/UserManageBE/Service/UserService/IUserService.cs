using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        IEnumerable<User> GetPaggingAndSearch(string searchText, int pageNumber, int pageSize, int gender, DateTime? fromDate, DateTime? toDate);

    }
}
