using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;
using DataLayer.DBContext;
using Swashbuckle.AspNetCore.Annotations;
using APIExtension.Const;
using ServiceLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using APIExtension.ClaimsPrinciple;
using APIExtension.UpdateApiExtension;
using ShareResource.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;

        public GroupsController(IServiceWrapper services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }

        // GET: api/Groups
        [SwaggerOperation(
           Summary = $"[{Actor.Test}/{Finnished.False}]Get list of group",
           Description = $"[{Actor.Test}/{Finnished.False}]Get list of group"
       )]
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            IQueryable<Group> list = services.Groups.GetList();
            if (list == null|| !list.Any())
          {
              return NotFound();
          }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Join
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.False}]Get list of group you joined",
           Description = "Get list of group you joined"
       )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Join")]
        public async Task<IActionResult> GetJoinedGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetGroupsJoinedByStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Lead
        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.False}] Get list of dto you lead",
           Description = "Get list of dto you lead"
       )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Lead")]
        public async Task<IActionResult> GetLeadGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetGroupsLeadByStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            return Ok(list);
        }

        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.False}] Get dto by Id",
          Description = "Get dto by Id"
      )]
        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            Group group = await services.Groups.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }
        // POST: api/Groups
        [Authorize(Roles =Actor.Student)]
        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(GroupCreateDto dto)
        {
            int creatorId = HttpContext.User.GetUserId();
            Group group = mapper.Map<Group>(dto);
            await services.Groups.CreateAsync(group, creatorId);

            return Ok(mapper);
        }

        // PUT: api/Groups/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupUpdateDto dto)
        {
            if (id != HttpContext.User.GetUserId())
            {
                return Unauthorized("You can't update other's profile");
            }
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var group = await services.Groups.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            try
            {
                group.PatchUpdate<Group, GroupUpdateDto>(dto);
                await services.Groups.UpdateAsync(group);
                return Ok(group);
            }
            catch (Exception ex)
            {
                if (!await GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        

        //// DELETE: api/Groups/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGroup(int id)
        //{
        //    if (services.Groups == null)
        //    {
        //        return NotFound();
        //    }
        //    var @dto = await services.Groups.FindAsync(id);
        //    if (@dto == null)
        //    {
        //        return NotFound();
        //    }

        //    services.Groups.Remove(@dto);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        private async Task<bool> GroupExists(int id)
        {
            return (await services.Groups.GetByIdAsync(id)) is not null;
        }
    }
}
