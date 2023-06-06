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
using APIExtension.Const;
using Swashbuckle.AspNetCore.Annotations;
using APIExtension.ClaimsPrinciple;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IServiceWrapper services;

        public MeetingsController(IServiceWrapper services)
        {
            this.services = services;
        }

        //GET: api/Meetings/Past/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.False}/{Auth.True}] Get all past meetings of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Past/Group/{groupId}")]
        public async Task<IActionResult> GetPastMeetingForGroup(int groupId)
        {
            int studentId=HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if(!isLeader)
            {
                 return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            var mapped = services.Meetings.GetPastMeetingsForGroup(groupId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Schedule/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.False}/{Auth.True}] Get all Schedule meetings of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Schedule/Group/{groupId}")]
        public async Task<IActionResult> GetScheduleMeetingForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            var mapped = services.Meetings.GetScheduleMeetingsForGroup(groupId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Schedule/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.False}/{Auth.True}] Get all Live meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Live/Group/{groupId}")]
        public async Task<IActionResult> GetLiveMeetingForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            var mapped = services.Meetings.GetLiveMeetingsForGroup(groupId);
            return Ok(mapped);
        }

        //// GET: api/Meetings
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings()
        //{
        //  if (services.Meetings. == null)
        //  {
        //      return NotFound();
        //  }
        //    return await services.Meetings.ToListAsync();
        //}

        //// GET: api/Meetings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Meeting>> GetMeeting(int id)
        //{
        //  if (services.Meetings == null)
        //  {
        //      return NotFound();
        //  }
        //    var meeting = await services.Meetings.FindAsync(id);

        //    if (meeting == null)
        //    {
        //        return NotFound();
        //    }

        //    return meeting;
        //}

        //// PUT: api/Meetings/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMeeting(int id, Meeting meeting)
        //{
        //    if (id != meeting.Id)
        //    {
        //        return BadRequest();
        //    }

        //    services.Entry(meeting).State = EntityState.Modified;

        //    try
        //    {
        //        await services.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MeetingExists(id))
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

        //// POST: api/Meetings
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
        //{
        //  if (services.Meetings == null)
        //  {
        //      return Problem("Entity set 'TempContext.Meetings'  is null.");
        //  }
        //    services.Meetings.Add(meeting);
        //    await services.SaveChangesAsync();

        //    return CreatedAtAction("GetMeeting", new { id = meeting.Id }, meeting);
        //}

        //// DELETE: api/Meetings/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMeeting(int id)
        //{
        //    if (services.Meetings == null)
        //    {
        //        return NotFound();
        //    }
        //    var meeting = await services.Meetings.FindAsync(id);
        //    if (meeting == null)
        //    {
        //        return NotFound();
        //    }

        //    services.Meetings.Remove(meeting);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        private async Task<bool> MeetingExists(int id)
        {
            return (await services.Meetings.AnyAsync(id));
        }
    }
}
