using FamilyApplication.DTOs;
using FamilyApplication.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> registerAdmin([FromForm]RegisterDTO registerDTO)
        {
            var admin = await _adminService.CreateAdmin(registerDTO);

            if (admin == null)
            {
                return NotFound("Admin account already exists.");
            }
            return Ok(admin);
        }
        [HttpPut("updateAdmin/{id}")]
        public async Task<IActionResult> UpdateAdmin([FromForm] UpdateProfileDTO updateProfileDTO, int id)
        {
            var admin =await _adminService.UpdateAdmin(updateProfileDTO, id); 

            if (admin == null)
            {
                return NotFound("admin not found");
            }
            return Ok(admin);   
        }  
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var user = await _adminService.ChangeUserStatus(id, "Approved");

            if (user == null)
            {
                return NotFound("user does not exit");
            }
            return Ok(user);
        }
        [HttpPut("Rejected/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            var user = await _adminService.ChangeUserStatus(id, "Rejected");

            if(user == null)
            {
                return NotFound("user does not exit");
            }
            return Ok(user);
        }
        [HttpGet("getalluser")]
        public async Task<IActionResult> getalluser()
        {
           var user = await _adminService.GetAllUser();
            if(user == null)
            {
                return BadRequest("not found");
            }
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet("adminInfo")]
        public async Task<IActionResult> AdminInfo()
        {
            var admin = await _adminService.ShowAdminInfo();
            if(admin == null)
            {
                return NotFound("admin not found");
            }
            return Ok(admin);
        }

    }
}
