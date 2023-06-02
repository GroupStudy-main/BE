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


        // GET: api/Groups/Join
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get list of groups student joined",
           Description = "Get list of groups student joined"
       )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Join")]
        public async Task<IActionResult> GetJoinedGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetMemberGroupsAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Lead
        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}] Get list of groups where student is leader",
           Description = "Get list of groups where student is leader"
       )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Lead")]
        public async Task<IActionResult> GetLeadGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetLeaderGroupsAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // POST: api/Groups
        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}] create new group for leader",
           Description = "create new group for leader"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(GroupCreateDto dto)
        {
            int creatorId = HttpContext.User.GetUserId();
            Group group = mapper.Map<Group>(dto);
            await services.Groups.CreateAsync(group, creatorId);

            return Ok();
        }

        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.False}] Get group by Id",
          Description = "Get group by Id"
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
        

        // PUT: api/Groups/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, GroupUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            int studentId = HttpContext.User.GetUserId();
            List<int> leadGroupIds = (await services.Groups.GetLeaderGroupsIdAsync(studentId));

            if (!leadGroupIds.Contains(id))
            {
                return Unauthorized("You can't update other's group");
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

        // GET: api/Groups
        [SwaggerOperation(
           Summary = $"[{Actor.Test}/{Finnished.True}]Get leadGroupIds of group",
           Description = "Get leadGroupIds of group"
       )]
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            IQueryable<Group> list = services.Groups.GetList();
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
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
