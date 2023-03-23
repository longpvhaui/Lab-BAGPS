using Domain.Models;
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

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll() 
        {
            var users =  _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("get-pagging")]
        public IActionResult GetPagging(string searchText, int pageNumber, int pageSize, int gender, string fromDate, string toDate) 
        {
            var fromBirthDate = new DateTime();
            var toBirthDate = new DateTime();
            fromBirthDate = DateTime.Parse(fromDate);
            toBirthDate = DateTime.Parse(toDate);
            var user = _userService.GetPaggingAndSearch(searchText, pageNumber, pageSize, gender, fromBirthDate, toBirthDate);
            return Ok(user);
        }

        [HttpPost]
        [Route("add")]
        public  IActionResult AddUser(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            _userService.InsertUser(user);
            return Ok();
        }
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
                currUser.LoginName = user.LoginName;
                currUser.Password = user.Password;
                currUser.Name = user.Name;
                currUser.Email = user.Email;
                currUser.Birthday = user.Birthday;
                currUser.Phone = user.Phone;
                _userService.UpdateUser(currUser);
                return Ok();
            }else return BadRequest();
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            var currUser = _userService.GetUser(id);
            if(currUser != null)
            {
                _userService.DeleteUser(currUser.Id);
                return Ok();
            }else return NotFound();

        }
    }
}
