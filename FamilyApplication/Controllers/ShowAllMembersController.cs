using FamilyApplication.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowAllMembersController : ControllerBase
    {
        private readonly IShowAllMembersService _showAllMembersService;

        public ShowAllMembersController(IShowAllMembersService showAllMembersService)
        {
            _showAllMembersService = showAllMembersService; 
        }
        [HttpGet("ShowAllMembers")]  
        public async Task<IActionResult> showallmember([FromQuery] string? search)
        {
         
            var members = await _showAllMembersService.ShowAllMembersAsync();

            if(!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                members = members.Where(u=>
                              u.FamilyHeadFName.ToLower().Contains(search) || 
                              u.FamilyHeadLName.ToLower().Contains(search) || 
                              u.Phone.ToLower().Contains(search) ||
                              u.Members.Any(m=>m.Name.ToLower().Contains(search)));
            }

            if (members == null ||!members.Any())
            {
                return NotFound("No members");
            }
            return Ok(members); 
        }
    }
}
