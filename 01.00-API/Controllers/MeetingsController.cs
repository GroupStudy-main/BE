﻿using AutoMapper;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ShareResource.DTO;
using APIExtension.Validator;
using AutoMapper;
using System.Collections;
using DataLayer.DBContext;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ShareResource.DTO.Meeting;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MeetingsController : ControllerBase
{
    private readonly IServiceWrapper _service;
    private readonly IMapper _mapper;

    public MeetingsController(IServiceWrapper services, IMapper mapper)
    {
        private readonly IServiceWrapper services;
        private readonly IValidatorWrapper validators;
        private readonly IMapper mapper;
        private readonly GroupStudyContext context;

        public MeetingsController(IServiceWrapper services, IValidatorWrapper validators, IMapper mapper, GroupStudyContext context)
        {
            this.services = services;
            this.validators = validators;
            this.mapper = mapper;
            this.context = context;
        }

        //GET: api/Meetings/Past/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Past/Group/{groupId}")]
        public async Task<IActionResult> GetPastMeetingForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            var mapped = services.Meetings.GetPastMeetingsForGroup(groupId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Schedule/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all Schedule meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
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
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all Live meetings of group"
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

        [SwaggerOperation(
          Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Create a new instant meeting"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Instant")]
        public async Task<IActionResult> CreateInstantMeeting(InstantMeetingCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.Meetings.CreateInstantMeetingAsync(dto);
            return Ok(dto);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Mass create many schedule meetings within a range of time",
            Description = "ScheduleSRangeStart: chỉ cần date, time ko quan trọng nhưng vẫn phải điền (cho 00:00:00)<br/>"
        )]
        //[Authorize(Roles = Actor.Student)]
        [HttpPost("Mass-schedule")]
        public async Task<IActionResult> MassCreateScheduleMeeting(ScheduleMeetingMassCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, 1 /*studentId*/);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }


            IEnumerable<Meeting> createdMeetings = await services.Meetings.MassCreateScheduleMeetingAsync(dto);

            //await services.Meetings.CreateScheduleMeetingAsync(dto);
            return Ok(createdMeetings);
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Create a new schedule meeting"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Schedule")]
        public async Task<IActionResult> CreateScheduleMeeting(ScheduleMeetingCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.Meetings.CreateScheduleMeetingAsync(dto);
            return Ok(dto);
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Start a schedule meeting"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Schedule/{id}/Start")]
        public async Task<IActionResult> StartSchdeuleMeeting(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            var meeting = await services.Meetings.GetByIdAsync(id);
            bool isJoining = await services.Groups.IsStudentJoiningGroupAsync(studentId, meeting.GroupId);
            if (!isJoining)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            if (meeting.End != null)
            {
                return BadRequest("Meeting đã kết thúc");
            }
            if (meeting.Start != null)
            {
                return BadRequest("Meeting đã bắt đầu");
            }
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId);
            //phải bắt đầu trong ngày schedule
            if (meeting.ScheduleStart.Value.Date > DateTime.Today)
            {
                return BadRequest($"Meeting được hẹn vào ngày {meeting.ScheduleStart.Value.Date.ToString("dd/MM")} Nếu muốn bắt đầu vào ngày hôm nay, hãy {(isLeader ? "cập nhật lại ngày hẹn" : "yêu  cầu nhóm trưởng cập nhật lại ngày hẹn")}");
            }
            //Member ko dc bắt sớm hơn
            if (meeting.ScheduleStart.Value > DateTime.Now && !isLeader)
            {
                return BadRequest($"Thành viên không thể bắt đầu meeting sớm hơn giờ hẹn. Nếu muốn bắt đầu ngay, hãy yêu  cầu nhóm trưởng bắt đầu cuộc họp");
            }
            meeting.Start = DateTime.Now;
            await services.Meetings.StartScheduleMeetingAsync(meeting);
            LiveMeetingGetDto dto = mapper.Map<LiveMeetingGetDto>(meeting);
            return Ok(dto);
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.No_Test}/{Auth.True}] Update a schedule meeting"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Schedule/{id}")]
        public async Task<IActionResult> UpdateScheduleMeeting(int id, ScheduleMeetingUpdateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            var meeting = await services.Meetings.GetByIdAsync(id);
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải thành viên của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.Meetings.UpdateScheduleMeetingAsync(dto);
            var updatedDto = mapper.Map<ScheduleMeetingGetDto>(await services.Meetings.GetByIdAsync(id));
            return Ok(updatedDto);
        }


        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Remove a schedule meeting"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpDelete("Schedule/{id}")]
        public async Task<IActionResult> DeleteSchdeuleMeeting(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            var meeting = await services.Meetings.GetByIdAsync(id);
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            if (meeting.End != null)
            {
                return BadRequest("Meeting đã kết thúc, không xóa được");
            }
            if (meeting.Start != null)
            {
                return BadRequest("Meeting đã bắt đầu, không xóa được");
            }
            //phải bắt đầu trong ngày schedule
            if (meeting.ScheduleStart.Value.Date < DateTime.Today)
            {
                return BadRequest("Meeting đã qua, không xóa được");
            }
            await services.Meetings.DeleteScheduleMeetingAsync(meeting);
            return Ok("Đã xóa meeting");
        }

        // GET: api/Meetings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings()
        {
            if (context.Meetings == null)
            {
                return NotFound();
            }
            return await context.Meetings.ToListAsync();
        }

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

    [HttpPost("/create-meeting")]
    public async Task<IActionResult> CreateMeeting(MeetingCreateDto dto)
    {
        if (null == dto.ScheduleStart || null == dto.ScheduleEnd)
        {
            return BadRequest("ScheduleTime must be not empty ");
        }

        if (null == dto.ScheduleStart || dto.ScheduleStart > DateTime.Now)
        {
            return BadRequest("ScheduleStart must be after " + DateTime.Now);
        }

        if (null == dto.ScheduleEnd || dto.ScheduleEnd < dto.ScheduleStart)
        {
            return BadRequest("ScheduleEnd must be after " + dto.ScheduleStart);
        }

        Meeting meeting = _mapper.Map<Meeting>(dto);
        await _service.Meeting.CreateMeeting(meeting);
        return Ok("Created Meeting");
    }

    // GET: api/Meetings
    [HttpGet("/get-list")]
    public async Task<ActionResult<MeetingGetDto>> GetMeeting([FromQuery] string? type)
    {
        if (String.IsNullOrEmpty(type))
        {
            type = "All";
        }

        var meetings = _service.Meeting.GetListMeeting(type).ToList();
        if (null == meetings || meetings.Count < 1)
        {
            return NotFound();
        }

        List<MeetingGetDto> result = new List<MeetingGetDto>();

        foreach (var item in meetings)
        {
            result.Add(_mapper.Map<MeetingGetDto>(item));
        }

        return Ok(result);
    }

    [HttpPut("/start")]
    public async Task<IActionResult> StartMeeting(int id)
    {
        var meeting = _service.Meeting.GetById(id).Result;
        if (null == meeting)
        {
            return NotFound();
        }

        meeting.Start = DateTime.Now;
        await _service.Meeting.UpdateMeeting(meeting);
        return Ok("Meeting started");
    }

    [HttpPut("/end")]
    public async Task<IActionResult> EndMeeting(int id)
    {
        var meeting = _service.Meeting.GetById(id).Result;
        if (null == meeting)
        {
            return NotFound();
        }

        meeting.End = DateTime.Now;
        await _service.Meeting.UpdateMeeting(meeting);
        return Ok("Meeting Ended");
    }
}