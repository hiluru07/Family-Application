using FamilyApplication.CommonServices;
using FamilyApplication.DTOs;
using FamilyApplication.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(IAuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;   
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _authService.Login(loginDTO);  

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }
            var token = _jwtService.GenerateToken(user.userId,user.username, user.Role);

            return Ok(new
            {
                token = token,
                id = user.userId,
                user = user.username,
                ProfileImage = "/Images/" + user.ProfileImage,
                Fname = user.Fname,
                Lname = user.Lname,
                Phone = user.Phone,
                Email = user.Email,
                role = user.Role,
                status = user.status,
                message = user.Message
            });
        }
        [HttpPost("verifyuser")]
        public async Task<IActionResult> verify(ForgetPassDTO forgetPassDTO)
        {
            var userId = await _authService.verifyuser(forgetPassDTO);

            if (userId == null)
                return BadRequest("User not found");

            return Ok(new { userId, message = "User verified successfully ✅" });
        }

        [HttpPut("update/{id}")]
        public async Task <IActionResult> updatepassword(int id, UpdatePasswordDTO updatePasswordDTO)
        {
            bool user = await _authService.updatepass(id,updatePasswordDTO);

            if(!user)
            {
                return BadRequest("Cannot update, User not found");
            }
            return Ok(new {user=id, message = "🔑 Password Updated Successfully" });
        }
    }
}
