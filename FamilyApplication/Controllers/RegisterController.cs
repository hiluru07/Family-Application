using FamilyApplication.CommonServices;
using FamilyApplication.DTOs;
using FamilyApplication.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly PasswordHasedService _passwordHasedService;
        private readonly JwtService _jwtService;
        public RegisterController(IRegisterService registerService, PasswordHasedService passwordHasedService, JwtService jwtService)
        {
            _registerService = registerService; 
            _passwordHasedService = passwordHasedService;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterDTO registerDTO)
        {
            var user = await _registerService.RegisterUser(registerDTO);
            if (user == null)
            {
                return BadRequest("User already exist");
            }
            return Ok(user);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var user = await _registerService.ViewUserById(id);
           
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _registerService.GetAllUser();
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);    
        }

        [HttpPut("modifyUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromForm]UpdateProfileDTO updateProfileDTO, int id)
        {
            var user = await _registerService.UpdateUser(updateProfileDTO, id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task <IActionResult> RemoveUser(int id)
        {
            var user = await _registerService.DeleteUser(id);
            if(!user)
                return NotFound();
            
            return Ok();
        }
    }
}
