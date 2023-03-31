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
        public ResponseService<User> DeleteUser(int id)
        {
            var response = new ResponseService<User>();
            User user = GetUser(id);
            if (user != null)
            {
                user.IsDelete = true;
                user.DeletedDate = DateTime.Now;
                _userRepository.SaveChanges();
                response.Data = user;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Delete Fail";
                response.Data = null;
                return response;
            }
            
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
        public ResponseUser GetUserSearch(SearchModel model)
        {
            var response = new ResponseUser();
            var users = GetAll();

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

            var userPagging = users.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToList();
            var totalItems = users.ToList().Count;
            var totalPages = Math.Ceiling((double)totalItems / model.PageSize);
            response.Users = userPagging;
            response.TotalItems = totalItems;
            response.TotalPages = totalPages;

            return response;
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
        public User GetUser(int id)
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
        public ResponseUser GetPagging(int pageIndex, int pageSize)
        {
            var response = new ResponseUser();
            var userAll = GetAll().ToList();
            var users = userAll.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var totalItems = userAll.Count;
            var totalPages = Math.Ceiling((double)totalItems / pageSize);
            response.Users = users;
            response.TotalItems = totalItems;
            response.TotalPages = totalPages;
                
           return response;
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
        public ResponseService<User> InsertUser(User user)
        {   
            var response = new ResponseService<User>();
            var users = GetAll();
            var userExist = users.Where(x => x.LoginName == user.LoginName).ToList();
            if (userExist.Count > 0)
            {
                response.Success = false;
                response.Message = "Đã tồn tại user";
                response.Data = null;
                return response;
            }
            else
            {
                user.Password = _md5.EncryptPassword(user.Password);
                _userRepository.Insert(user);
                response.Data = user;
                return response;
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
        public ResponseService<User> UpdateUser(User user)
        {
            var response = new ResponseService<User>();
            try
            {
                _userRepository.Update(user);
                response.Data = user;
                return response;
            }catch (Exception ex)
            {
                response.Success =false;
                response.Message = ex.Message;
                return response;
            }
            
        }

        public  IEnumerable<User> GetAll()
        {
            var users = _userRepository.GetAll().Where(x => x.IsDelete == false).OrderBy(x => x.LoginName);
            return users;
        }
    }
}
