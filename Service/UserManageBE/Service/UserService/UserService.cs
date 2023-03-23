using Azure;
using Domain.Models;
using Infrastructure.Encrypt;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private readonly IMD5Encrypt _md5;

        public UserService(IRepository<User> userRepository, IMD5Encrypt md5)
        {
            _userRepository = userRepository;
            _md5 = md5;
        }
        public void DeleteUser(int id)
        {
            User user = GetUser(id);
            user.IsDelete = true;
            _userRepository.SaveChanges();
        }

        public IEnumerable<User> GetPaggingAndSearch(string searchText, int pageNumber, int pageSize, int gender, DateTime? fromDate, DateTime? toDate)
        {
            var users = GetUsers();

            if (!string.IsNullOrEmpty(searchText))
            {
                users = users.Where(x => x.ToString().Contains(searchText));
            }

            if (gender != null)
            {
                users = users.Where(x => x.Gender == gender);
            }

            if (fromDate != null)
            {
                users = users.Where(x => x.Birthday >= fromDate);
            }

            if (toDate != null)
            {
                users = users.Where(x => x.Birthday <= toDate);
            }

            return  users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public User? GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            if(!user.IsDelete) return user;
            else return null;
        }

        public IEnumerable<User> GetUsers()
        {
           var  users =  _userRepository.GetAll().Where(x=>x.IsDelete == false);
            return users;
        }

        public void InsertUser(User user)
        {
            user.Password = _md5.EncryptPassword(user.Password);
             _userRepository.Insert(user);

        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
