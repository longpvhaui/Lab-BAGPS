using Azure;
using Domain.Models;
using Infrastructure.Encrypt;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private readonly IMD5Encrypt _md5;

        public UserService(IRepository<User> userRepository, IMD5Encrypt md5)
        {
            _userRepository = userRepository;
            _md5 = md5;
        }
        /// <summary>Xóa nhân viên</summary>
        /// <param name="id">The identifier.</param>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public User? DeleteUser(int id)
        {
            User user = GetUser(id);
            if (user != null)
            {
                user.IsDelete = true;
                user.DeletedDate = DateTime.Now;
                _userRepository.SaveChanges();
                return user;
            }
            else return null;
        }

        /// <summary>Tìm kiếm theo tên, ngày sinh, giới tính</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public IEnumerable<User> GetPaggingAndSearch(SearchModel model)
        {
            var users = GetUsers();

            if (!string.IsNullOrEmpty(model.SearchText))
            {
                var searchText = model.SearchText.ToLower();
                 users = users.Where(x => x.Name.ToLower().Contains(searchText) || x.Phone.Contains(searchText) || x.Email.Contains(searchText));
            }

            if (model.Gender is not null)
            {
                users = users.Where(x => x.Gender == Int32.Parse(model.Gender));
            }

            if (!string.IsNullOrEmpty(model.FromDate) && DateTime.TryParseExact(model.FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthFrom))
            {
                users = users.Where(x => x.Birthday >= birthFrom);
            }

            if (model.ToDate != null && DateTime.TryParseExact(model.ToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthTo))
            { 
                users = users.Where(x => x.Birthday <= birthTo);
            }

            return users;
        }

        /// <summary>
        ///   <para>Lấy ra 1 nhân viên theo id</para>
        ///   <para>
        ///     <br />
        ///   </para>
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public User? GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            if(!user.IsDelete) return user;
            else return null;
        }

        /// <summary>Lấy ra toàn bộ nhân viên</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public IEnumerable<User> GetUsers()
        {
           var  users =  _userRepository.GetAll().Where(x=>x.IsDelete == false);
            return users;
        }

        /// <summary>Thêm nhân viên</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public User? InsertUser(User user)
        {
            var users = GetUsers();
            var userExist = users.Where(x => x.LoginName == user.LoginName).ToList();
            if (userExist.Count > 0)
            {
                return null;
            }
            else
            {
                user.Password = _md5.EncryptPassword(user.Password);
                _userRepository.Insert(user);
                return user;
            }

        }

        /// <summary>
        ///   <para>
        /// Cập nhật nhân viên</para>
        ///   <para>
        ///     <br />
        ///   </para>
        /// </summary>
        /// <param name="user">The user.</param>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        public User? UpdateUser(User user)
        {
            var users = GetUsers();
            var userExist = users.Where(x => x.LoginName == user.LoginName).ToList();
            if (userExist.Count > 0)
            {
                return null;
            }
            else
            {
                _userRepository.Update(user);
                return user;
            }
        }
    }
}
