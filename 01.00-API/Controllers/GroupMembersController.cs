using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;
using ServiceLayer.Interface;
using ShareResource.DTO;
using APIExtension.Const;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using APIExtension.ClaimsPrinciple;
using APIExtension.Validator;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ShareResource.Enums;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IValidatorWrapper validators;
        private readonly IMapper mapper;

        public GroupMembersController(IServiceWrapper services, IValidatorWrapper validators, IMapper mapper)
        {
            this.services = services;
            this.validators = validators;
            this.mapper = mapper;
        }

        //GET: api/GroupMember/Group/{groupId}
        [SwaggerOperation(
            Summary = $"[{Actor.Leader_Member}/{Finnished.True}/{Auth.True}] Get all members joining of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Group/{groupId}")]
        public async Task<IActionResult> GetJoinMembersForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            IQueryable<AccountProfileDto> mapped = services.GroupMembers.GetMembersJoinForGroup(groupId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //GET: api/GroupMember/Invite/Group/groupId
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all invite of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Invite/Group/{groupId}")]
        public async Task<IActionResult> GetInviteForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            IQueryable<GroupMemberInviteGetDto> mapped = services.GroupMembers.GetJoinInviteForGroup(groupId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //GET: api/GroupMember/Request/Group/groupId
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all join request of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Request/Group/{groupId}")]
        public async Task<IActionResult> GetRequestForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            IQueryable<JoinRequestGetDto> mapped = services.GroupMembers.GetJoinRequestForGroup(groupId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //Post: api/GroupMember/Invite
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Create Invite to join group for leader"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Invite")]
        public async Task<IActionResult> CreateInvite(GroupMemberInviteCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            #region unused code
            //if (await services.Groups.IsStudentJoiningGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã tham gia nhóm này");
            //    return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
            //}
            //if (await services.Groups.IsStudentInvitedToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã được mời tham gia nhóm này từ trước");
            //    GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>( 
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { Message = "Học sinh đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
            //}
            //if (await services.Groups.IsStudentRequestingToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã yêu cầu tham gia nhóm này từ trước");
            //    GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { Message = "Học sinh đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
            //}
            //if (await services.Groups.IsStudentDeclinedToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước");
            //    GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { 
            //        Message = "Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại", 
            //        Previous = getDto 
            //    });
            //}
            #endregion
            GroupMember exsited = await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
            if (exsited!=null) {
                switch (exsited.State)
                {
                    case GroupMemberState.Leader:
                        {
                            return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
                        }
                    case GroupMemberState.Member:
                        {
                            return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
                        }
                        //Fix later
                    //case GroupMemberState.Inviting:
                    //    {
                    //        GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Học sinh đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
                    //    }
                    //case GroupMemberState.Requesting:
                    //    {
                    //        GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Học sinh đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
                    //    }
                    case GroupMemberState.Banned:
                        {
                            GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                                await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                            return BadRequest(new
                            {
                                Message = "Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại",
                                Previous = getDto
                            });
                        }
                    default:
                        {
                            GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                                await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                            return BadRequest(new
                            {
                                Message = "Học sinh đã có liên quan đến nhóm",
                                Previous = getDto
                            });
                        }
                } 
            }
            ValidatorResult valResult = await validators.GroupMembers.ValidateParamsAsync(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.GroupMembers.CreateJoinInvite(dto);

            return Ok();
        }

        //Put: api/GroupMember/Request/{inviteId}/Accept"
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Accept join request for leader"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Request/{inviteId}/Accept")]
        public async Task<IActionResult> AcceptRequest(int inviteId)
        {
            int studentId = HttpContext.User.GetUserId();
            GroupMember existed = await services.GroupMembers.GetByIdAsync(inviteId);
            if (existed == null)
            {
                return NotFound("Yêu cầu không tồn tại");
            }
            if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, existed.GroupId))
            {
                return BadRequest("Bạn không phải trưởng nhóm này");
            }
            //if (existed.State != GroupMemberState.Requesting)
            //{
            //    return BadRequest("Đây không phải yêu cầu");
            //}
            await services.GroupMembers.AcceptOrDeclineRequestAsync(existed, true);
            return Ok();
        }


        //Put: api/GroupMember/Invite/{inviteId}/Decline"
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all join request of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Request/{inviteId}/Decline")]
        public async Task<IActionResult> DeclineRequest(int inviteId)
        {
            int studentId = HttpContext.User.GetUserId();
            GroupMember existed = await services.GroupMembers.GetByIdAsync(inviteId);
            if (existed == null)
            {
                return NotFound("Yêu cầu không tồn tại");
            }
            if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, existed.GroupId))
            {
                return BadRequest("Bạn không phải trưởng nhóm này");
            }
            //if (existed.State != GroupMemberState.Requesting)
            //{
            //    return BadRequest("Đây không phải yêu cầu");
            //}
            await services.GroupMembers.AcceptOrDeclineRequestAsync(existed, false);
            return Ok();
        }

        //GET: api/GroupMember/Invite/Student/{studentId}
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all join invite of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Invite/Student")]
        public async Task<IActionResult> GetInviteForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<GroupMemberInviteGetDto> mapped = services.GroupMembers.GetJoinInviteForStudent(studentId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return base.Ok(mapped);
        }

        //GET: api/GroupMember/Request/Student/{studentId}
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all request of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Request/Student")]
        public async Task<IActionResult> GetRequestForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<JoinRequestGetDto> mapped = services.GroupMembers.GetJoinRequestForStudent(studentId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //POST: api/GroupMember/Request
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all request of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Request")]
        public async Task<IActionResult> CreateRequest(GroupMemberRequestCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (studentId!=dto.AccountId)
            {
                return Unauthorized("Bạn không thể yêu cầu tham gia dùm người khác");
            }
            #region unused code
            //if (await services.Groups.IsStudentJoiningGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã tham gia nhóm này");
            //    return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
            //}
            //if (await services.Groups.IsStudentInvitedToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã được mời tham gia nhóm này từ trước");
            //    GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>( 
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { Message = "Học sinh đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
            //}
            //if (await services.Groups.IsStudentRequestingToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã yêu cầu tham gia nhóm này từ trước");
            //    GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { Message = "Học sinh đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
            //}
            //if (await services.Groups.IsStudentDeclinedToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước");
            //    GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { 
            //        Message = "Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại", 
            //        Previous = getDto 
            //    });
            //}
            #endregion
            GroupMember exsited = await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
            if (exsited != null)
            {
                switch (exsited.State)
                {
                    case GroupMemberState.Leader:
                        {
                            return BadRequest(new { Message = "Bạn đã tham gia nhóm này" });
                        }
                    case GroupMemberState.Member:
                        {
                            return BadRequest(new { Message = "Bạn đã tham gia nhóm này" });
                        }
                        //Fix later
                    //case GroupMemberState.Inviting:
                    //    {
                    //        GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Bạn đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
                    //    }
                    //case GroupMemberState.Requesting:
                    //    {
                    //        GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Bạn đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
                    //    }
                    case GroupMemberState.Banned:
                        {
                            GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                                await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                            return BadRequest(new
                            {
                                Message = "Bạn đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại",
                                Previous = getDto
                            });
                        }
                    default:
                        {
                            GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                                await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                            return BadRequest(new
                            {
                                Message = "Bạn đã có liên quan đến nhóm",
                                Previous = getDto
                            });
                        }
                }
            }
            ValidatorResult valResult = await validators.GroupMembers.ValidateParamsAsync(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.GroupMembers.CreateJoinRequest(dto);

            return Ok();
        }

        //Put: api/GroupMember/Invite/{inviteId}/Accept"
        [SwaggerOperation(
            Summary = $"[{Actor.Member}/{Finnished.True}/{Auth.True}] Get all join request of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Invite/{inviteId}/Accept")]
        public async Task<IActionResult> AcceptInvite(int inviteId)
        {
            int studentId = HttpContext.User.GetUserId();
            GroupMember existed = await services.GroupMembers.GetByIdAsync(inviteId);
            if (existed == null)
            {
                return NotFound("Lời mời không tồn tại");
            }
            if (existed.AccountId != studentId)
            {
                return BadRequest("Đây không phải lời mời cho bạn");
            }
            //Fix later
            //if (existed.State != GroupMemberState.Inviting)
            //{
            //    return BadRequest("Đây không phải lời mời");
            //}
            await services.GroupMembers.AcceptOrDeclineInviteAsync(existed, true);
            return Ok();
        }


        //Put: api/GroupMember/Invite/{inviteId}/Decline"
        [SwaggerOperation(
            Summary = $"[{Actor.Member}/{Finnished.True}/{Auth.True}] Get all join request of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Invite/{inviteId}/Decline")]
        public async Task<IActionResult> DeclineInvite(int inviteId)
        {
            int studentId = HttpContext.User.GetUserId();
            GroupMember existed = await services.GroupMembers.GetByIdAsync(inviteId);
            if (existed == null)
            {
                return NotFound("Lời mời không tồn tại");
            }
            if (existed.AccountId != studentId)
            {
                return BadRequest("Đây không phải lời mời cho bạn");
            }
            //if (existed.State != GroupMemberState.Inviting)
            //{
            //    return BadRequest("Đây không phải lời mời");
            //}
            await services.GroupMembers.AcceptOrDeclineInviteAsync(existed, false);
            return Ok();
        }

        //////////////////////////////////////////////////////////////////// 
        //// GET: api/GroupMembers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GroupMember>>> GetGroupMembers()
        //{
        //  if (services.GroupMembers == null)
        //  {
        //      return NotFound();
        //  }
        //    return await services.GroupMembers.ToListAsync();
        //}

        //// GET: api/GroupMembers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GroupMember>> GetGroupMember(int id)
        //{
        //  if (services.GroupMembers == null)
        //  {
        //      return NotFound();
        //  }
        //    var groupMember = await services.GroupMembers.FindAsync(id);

        //    if (groupMember == null)
        //    {
        //        return NotFound();
        //    }

        //    return groupMember;
        //}

        //// PUT: api/GroupMembers/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGroupMember(int id, GroupMember groupMember)
        //{
        //    if (id != groupMember.Id)
        //    {
        //        return BadRequest();
        //    }

        //    services.Entry(groupMember).State = EntityState.Modified;

        //    try
        //    {
        //        await services.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GroupMemberExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/GroupMembers
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<GroupMember>> PostGroupMember(GroupMember groupMember)
        //{
        //  if (services.GroupMembers == null)
        //  {
        //      return Problem("Entity set 'TempContext.GroupMembers'  is null.");
        //  }
        //    services.GroupMembers.Add(groupMember);
        //    await services.SaveChangesAsync();

        //    return CreatedAtAction("GetGroupMember", new { id = groupMember.Id }, groupMember);
        //}

        //// DELETE: api/GroupMembers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGroupMember(int id)
        //{
        //    if (services.GroupMembers == null)
        //    {
        //        return NotFound();
        //    }
        //    var groupMember = await services.GroupMembers.FindAsync(id);
        //    if (groupMember == null)
        //    {
        //        return NotFound();
        //    }

        //    services.GroupMembers.Remove(groupMember);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        private async Task<bool> GroupMemberExists(int id)
        {
            return (await services.GroupMembers.AnyAsync(id));
        }
    }
}
