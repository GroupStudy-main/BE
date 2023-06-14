using AutoMapper;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ShareResource.DTO.Meeting;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentFileController : ControllerBase
{
    private readonly IServiceWrapper _service;
    private readonly IMapper _mapper;

    //Path của thư mục chứa file
    private const string path = "D:\\UploadFile";
    // dòng này đổi thành host của máy
    private const string HostUploadFile = "http://192.168.0.3:8080/";

    public DocumentFileController(IServiceWrapper services, IMapper mapper)
    {
        this._service = services;
        this._mapper = mapper;
    }

    [HttpPost("/upload-file/{meetingId}")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult> UploadFile(IFormFile file, [FromRoute] int meetingId)
    {
        string httpFilePath = "";
        try
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    httpFilePath = HostUploadFile + file.FileName;
                }
            }

            if (!String.IsNullOrEmpty(httpFilePath))
            {
                var documentFile = new DocumentFile();
                documentFile.HttpLink = httpFilePath;
                documentFile.Approved = false;
                documentFile.MeetingId = meetingId;
                await _service.DocumentFiles.CreateDocumentFile(documentFile);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }

        return Ok(httpFilePath);
    }

    // GET: api/Meetings
    [HttpGet("/get-list-file")]
    public async Task<ActionResult<MeetingGetDto>> DownloadFile([FromQuery] string? type)
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

    [HttpPut("/accept-file")]
    public async Task<IActionResult> AcceptFile(int id)
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
}