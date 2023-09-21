using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using NewProject.Reponsitories;

namespace NewProject.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]

    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepo;

        public RoleController(IRoleRepository repo)
        {
            _roleRepo = repo;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
          
                return Ok(await _roleRepo.GetAllRoleAsync());

        }

        [Authorize(Roles = "Admin,User")]      
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleByID(int roleId)
        {
            var role = await _roleRepo.GetRoleAsync(roleId);
            return role == null ? NotFound() : Ok(role);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewRole(RequestRoleModel model)
        {
            var newRoleId = await _roleRepo.AddRoleAsync(model);
            var role = await _roleRepo.GetRoleAsync(newRoleId);
            return role == null ? BadRequest() : Ok(role);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateSport(int roleId, [FromBody] RoleModel model)
        {
            if (roleId != model.RoleID)
            {
                return NotFound();
            }
            await _roleRepo.UpdateRoleAsync(roleId, model);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteSport(int roleId)
        {
            var role = await _roleRepo.GetRoleAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            await _roleRepo.DeleteRoleAsync(roleId);
            return Ok();
        }
    }
}
