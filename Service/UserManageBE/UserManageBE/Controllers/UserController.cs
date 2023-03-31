using Domain.Models;
using Infrastructure.Authorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.UserService;


namespace UserManageBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService) 
        {
            _userService = userService;   
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll(int pageIndex, int pageSize)
        {
            var response = _userService.GetPagging(pageIndex,pageSize);
            return Ok(response);
        }

        /// <summary>Tìm kiếm</summary>
        /// <param name="model">The model.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        [Authorize]
        [HttpPost]
        [Route("get-search")]
        public IActionResult GetPagging(SearchModel model)
        {

           var response = _userService.GetUserSearch(model);
            return Ok(response);
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
        [Authorize]
        [HttpPost]
        [Route("add")]
        public  IActionResult AddUser(User user)
        {
            user.CreatedDate = DateTime.Now;
            if(user == null)
            {
                return BadRequest();
            }
            var result  = _userService.InsertUser(user);
            return Ok(result);
        }

        /// <summary>Cập nhật nhân viên</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        [Authorize]
        [HttpPost]
        [Route("edit")]
        public IActionResult EditUser(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            var currUser = _userService.GetUser(user.Id);
            if(currUser != null)
            {
    

                currUser.Name = user.Name;
                currUser.Gender = user.Gender;
                currUser.Email = user.Email;
                currUser.Birthday = user.Birthday;
                currUser.Phone = user.Phone;
                var result = _userService.UpdateUser(currUser);
                return Ok(result);
            }else return BadRequest();
        }

        /// <summary>Xóa nhân viên</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            var currUser = _userService.GetUser(id);
            if(currUser != null)
            {
                var result = _userService.DeleteUser(currUser.Id);
                return Ok(result);
            }else return NotFound();

        }


        /// <summary>Xóa nhiều nhân viên 1 lúc</summary>
        /// <param name="ids">The ids.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/30/2023 created
        /// </Modified>
        [Authorize]
        [HttpPost]
        [Route("delete-multi")]
        public IActionResult DeleteMultiUser(int[] ids)
        {
            var isSuccess = false;
            foreach (var userId in ids)
            {
                var currUser = _userService.GetUser(userId);
                if (currUser != null)
                {
                    var result = _userService.DeleteUser(currUser.Id);
                   if(result != null) { isSuccess = true; }
                   else { isSuccess = false; }
                }
                else isSuccess = false;
            }
            if (isSuccess)
            {
                return Ok();
            }else return BadRequest();
        

        }
    }
}
