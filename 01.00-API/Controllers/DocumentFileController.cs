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

    [HttpPost("/upload-file/{groupId}")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult> UploadFile(IFormFile file, [FromRoute] int groupId)
    {
        string httpFilePath = "";
        var documentFile = new DocumentFile();
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
                documentFile.HttpLink = httpFilePath;
                documentFile.Approved = false;
                documentFile.GroupId = groupId;
                documentFile.CreatedDate = DateTime.Now;
                await _service.DocumentFiles.CreateDocumentFile(documentFile);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }

        return Ok(documentFile);
    }

    // GET: api/Meetings
    [HttpGet("/get-list-file")]
    public async Task<ActionResult<DocumentFile>> GetListFile()
    {
        var result = _service.DocumentFiles.GetList();

        return Ok(result);
    }
    
    // GET: api/Meetings
    [HttpGet("/get-detail")]
    public async Task<ActionResult<DocumentFile>> GetFileDetail([FromQuery] int id)
    {
        var result = await _service.DocumentFiles.GetById(id);
        if (null == result)
        {
            return NotFound();
        }
        return Ok(result);
    }

    // GET: api/Meetings
    [HttpGet("/get-file-by-group")]
    public async Task<ActionResult<DocumentFile>> GetListFileByGroupId([FromQuery] int groupId, bool? approved)
    {
        var meeting = _service.Meeting.GetById(groupId).Result;
        if (null == meeting)
        {
            return NotFound();
        }
        var result = _service.DocumentFiles.GetListByGroupId(groupId, approved);

        return Ok(result);
    }

    [HttpPut("/accept-file")]
    public async Task<IActionResult> UpdateFile(int id, bool approved)
    {
        var file = _service.DocumentFiles.GetById(id).Result;
        if (null == file)
        {
            return NotFound();
        }

        if (approved)
        {
            file.Approved = approved;
            await _service.DocumentFiles.UpdateDocumentFile(file);
        }

        return Ok("approved");
    }

    [HttpDelete("/delete-file")]
    //chỗ này thêm Authen
    public async Task<IActionResult> DeleteFIle(int id)
    {
        var file = _service.DocumentFiles.GetById(id).Result;
        if (null == file)
        {
            return NotFound();
        }

        await _service.DocumentFiles.DeleteDocumentFile(id);
        return Ok("Deleted");
    }
}