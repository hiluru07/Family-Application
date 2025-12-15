using System.Security.Claims;
using FamilyApplication.DTOs;
using FamilyApplication.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [Authorize(Roles ="FamilyHead,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService; 
        }
        
        // 🔹 Add single member
        [HttpPost("add-member")]
        public async Task<IActionResult> AddMember([FromForm] MemberDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var member = await _memberService.AddSingleMember(dto, userId);

            if (member == null)
                return BadRequest("Member not added");

            return Ok(new { message = "Member Added Successfully" });
        }

        // 🔹 Add multiple members (Admin)
        [HttpPost("add-members-admin")]
        public async Task<IActionResult> AddMembersAdmin([FromForm] MemberCreateModel model)
        {
            if (model == null || model.UserId == 0 || model.Members == null || !model.Members.Any())
                return BadRequest("No members added");

          var addedMembers = await _memberService.AddMultipleMembers(model);
            return Ok(new
            {
                message = "Members added successfully",
                members = addedMembers,
            });
        }

        [HttpGet("getmember")]
        public async Task<IActionResult> getMember()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var member = await _memberService.GetMember(userId);
            if (member == null)
            {
                return BadRequest();
            }
            return Ok(member);
        }

        [HttpDelete("deletemember/{id}")]
        public async Task<IActionResult> Deletemember(int id)
        {
            var member = await _memberService.DeleteMember(id);
            if (!member)
            {
                return NotFound();
            }
            return Ok(new { message = "Member Deleted Successfully" });


        }
        [HttpPut("updateMember/{id}")]
        public async Task<IActionResult> updateMember([FromForm]MemberDTO memberDTO, int id, [FromForm] string? RemoveImage)
        {
            var member = await _memberService.UpdateMember(memberDTO,id,RemoveImage);
            if(member == null)
            {
                return BadRequest("member not found");
            }
            
            return Ok(member);
        }
        
    }
}
