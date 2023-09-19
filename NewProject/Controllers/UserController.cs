using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.Data;
using NewProject.Models;
using NewProject.Models.Authentication;
using NewProject.Models.Authentication.Login;
using NewProject.Reponsitories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository repo)
        {
            _userRepo = repo;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]       
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(await _userRepo.GetAllUserAsync());
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetUserByID(int userID)
        {
            var user = await _userRepo.GetUserAsync(userID);
            return user == null ? NotFound() : Ok(user);
        }


        /*
        [HttpPost]
        public async Task<IActionResult> AddNewUser(RequestUserModel model)
        {
            
            var newUserId = await _userRepo.AddUserAsync(model);
            var user = await _userRepo.GetUserAsync(newUserId);
            return user == null ? BadRequest() : Ok(user);

        }
        */

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{userID}")]
        public async Task<IActionResult> UpdateUser(int userID, [FromBody] UserModel model)
        {
            if (userID != model.UserID)
            {
                return NotFound();
            }
            await _userRepo.UpdateUserAsync(userID, model);
            return Ok();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{userID}")]
        public async Task<IActionResult> DeleteUser(int userID)
        {
            var user = await _userRepo.GetUserAsync(userID);
            if (user == null)
                return NotFound();
            await _userRepo.DeleteUserAsync(userID);
            return Ok();
        }
    }
}
