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
using ShareResource.UpdateApiExtension;
using ShareResource.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using APIExtension.Validator;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;
        private readonly IValidatorWrapper validators;

        public GroupsController(IServiceWrapper services, IMapper mapper, IValidatorWrapper validators)
        {
            this.services = services;
            this.mapper = mapper;
            this.validators = validators;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
          if (dbContext.Groups == null)
          {
              return NotFound();
          }
            return await dbContext.Groups.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
          if (dbContext.Groups == null)
          {
              return NotFound();
          }
            var @group = await dbContext.Groups.FindAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // GET: api/Groups/Join
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get list of groups student joined",
           Description = "Get list of groups student joined as leader or member"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Join")]
        public async Task<IActionResult> GetJoinedGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetJoinGroupsOfStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Member
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get list of groups student joined as a member",
           Description = "Get list of groups student joined as member"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Member")]
        public async Task<IActionResult> GetMemberGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetMemberGroupsOfStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Member
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get group detail for a member",
           Description = "Get group detail for a member"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Member/{id}")]
        public async Task<IActionResult> GetGroupDetailForMember(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentMemberGroupAsync(studentId, id);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            Group group = await services.Groups.GetFullByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }
            GroupGetDetailForMemberDto dto = mapper.Map<GroupGetDetailForMemberDto>(group);
            return Ok(dto);
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
            IQueryable<Group> list = await services.Groups.GetLeaderGroupsOfStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Lead/5
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get group detail for leader by Id",
            Description = "Get group detail for leader by Id"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Lead/{id}")]
        public async Task<IActionResult> GetGroupDetailForLeader(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, id);
            if (!isLeader)
            {
                 return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            Group group = await services.Groups.GetFullByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }
            GroupGetDetailForLeaderDto dto = mapper.Map<GroupGetDetailForLeaderDto>(group);
            return Ok(dto);
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
            ValidatorResult valResult = await validators.Groups.ValidateParams(dto);
            if(!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            Group group = mapper.Map<Group>(dto);
            await services.Groups.CreateAsync(group, creatorId);

            return Ok();
        }

      


        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.Id)
            {
                return BadRequest();
            }

            if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, id))
            {
                return Unauthorized("You can't update other's group");
            }
            ValidatorResult valResult = await validators.Groups.ValidateParams(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
          if (dbContext.Groups == null)
          {
              return Problem("Entity set 'TempContext.Groups'  is null.");
          }
            dbContext.Groups.Add(@group);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.Id }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (dbContext.Groups == null)
            {
                return NotFound();
            }
            var @group = await dbContext.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            dbContext.Groups.Remove(@group);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return (dbContext.Groups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
