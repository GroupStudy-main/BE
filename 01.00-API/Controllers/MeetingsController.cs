using AutoMapper;
using DataLayer.DBObject;
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
        this._service = services;
        this._mapper = mapper;
    }
    
    [HttpPost("/create-meeting")] 
    public async Task<IActionResult> CreateMeeting(MeetingCreateDto dto)
    {
        if (null == dto.ScheduleStart || null == dto.ScheduleEnd)
        {
            return BadRequest("ScheduleTime must be not empty ");
        }
        if (dto.ScheduleStart > DateTime.Now)
        {
            return BadRequest("ScheduleStart must be after " + DateTime.Now);
        }
        if (dto.ScheduleEnd < dto.ScheduleStart)
        {
            return BadRequest("ScheduleEnd must be after " + dto.ScheduleStart);
        }
        Meeting meeting = _mapper.Map<Meeting>(dto);
        await _service.Meeting.CreateMeeting(meeting);
        return Ok("Created Meeting");
    }
    
    // GET: api/Meetings
    [HttpGet("/get-list")]
    public async Task<ActionResult<MeetingGetDto>> GetMeeting()
    {
        var meetings = _service.Meeting.GetListMeeting().ToList();
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