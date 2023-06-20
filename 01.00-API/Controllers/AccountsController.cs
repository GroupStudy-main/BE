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
using API.SignalRHub.Tracker;
using API.SignalRHub;
using Microsoft.AspNetCore.SignalR;
using RepositoryLayer.Interface;
using ShareResource.DTO.Connection;
using ShareResource.DTO;
using Microsoft.AspNetCore.Authorization;
using ServiceLayer.Interface;
using ShareResource.Enums;
using APIExtension.ClaimsPrinciple;
using ShareResource.UpdateApiExtension;
using Swashbuckle.AspNetCore.Annotations;
using APIExtension.Const;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using APIExtension.Validator;
using Microsoft.IdentityModel.Tokens;
using ShareResource;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly GroupStudyContext context;
        private readonly IRepoWrapper unitOfWork;
        private readonly IHubContext<PresenceHub> presenceHub;
        private readonly PresenceTracker presenceTracker;
        private readonly IMapper mapper;
        private readonly IValidatorWrapper validators;

        public AccountsController(IServiceWrapper services, IRepoWrapper unitOfWork, IHubContext<GroupHub> presenceHub, PresenceTracker presenceTracker, IMapper mapper, IValidatorWrapper validators)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.presenceHub = presenceHub;
            this.presenceTracker = presenceTracker;
            this.mapper = mapper;
            this.validators = validators;
        }

        //Get: api/Accounts/search
        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Search students by id, username, mail, Full Name",
            Description = "Để search thêm thành viên mới cho group, thêm groupId để loại ra hết những student đã liên quan đến nhóm"
        )]
        [Authorize(Roles = Actor.Student_Parent)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchStudent(string search, int? groupId)
        {
            var list = services.Accounts.SearchStudents(search, groupId);//.ToList();

            if (list == null)
            {
                return NotFound();
            }
            //var test = list.FirstOrDefault();
            var mapped = list.ProjectTo<AccountProfileDto>(mapper.ConfigurationProvider);
            //var mapped = mapper.Map<List<AccountProfileDto>>(list);
            return Ok(mapped);
        }

        // GET: api/Accounts/5
        [SwaggerOperation(
          Summary = $"[{Actor.Student_Parent}/{Finnished.True}/{Auth.True}] Get the self profile"
        )]
        [Authorize(Roles = Actor.Student_Parent)]
        //[Authorize(Roles = "Student, Parent")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            int id = HttpContext.User.GetUserId();
            var user = await services.Accounts.GetProfileByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
                  var mapped = mapper.Map<AccountProfileDto>(user);
            return Ok(mapped);
        }


        // PUT: api/Accounts/5
        [SwaggerOperation(
          Summary = $"[{Actor.Student_Parent}/{Finnished.True}/{Auth.True}] Update logined profile"
        )]
        [Authorize(Roles = "Student, Parent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, AccountUpdateDto dto)
        {
            if (id != HttpContext.User.GetUserId())
            {
                return Unauthorized("Không thể thay đổi profile của người khác");
            }
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var account = await services.Accounts.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ValidatorResult valResult = await validators.Accounts.ValidateParams(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            try
            {
                account.PatchUpdate<Account, AccountUpdateDto>(dto);
                await services.Accounts.UpdateAsync(account);
                return Ok(account);
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // PUT: api/Accounts/5/Password
        [SwaggerOperation(
          Summary = $"[{Actor.Student_Parent}/{Finnished.True}/{Auth.True}] Update account password"
        )]
        [Authorize(Roles = Actor.Student_Parent)]
        [HttpPut("{id}/Password")]
        public async Task<IActionResult> ChangePassword(int id, AccountChangePasswordDto dto)
        {
            if (id != HttpContext.User.GetUserId())
            {
                return Unauthorized("Không thể thay đổi mật khẩu của người khác");
            }
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (dto.Password != dto.ConfirmPassword)
            {
                return BadRequest(FAIL_CONFIRM_PASSWORD_MSG);
            }

            var account = await services.Accounts.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            if(dto.OldPassword != dto.Password)
            {
                return Unauthorized("Nhập mật khẩu cũ thất bại");
            }
            try
            {
                account.PatchUpdate<Account, AccountChangePasswordDto>(dto);
                await services.Accounts.UpdateAsync(account);
                return Ok(account);
            }
            catch (Exception ex)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        [Tags(Actor.Test)]
        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.False}] Get all the account"
        )]
        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetUser()
        {
            IQueryable<Account> list = services.Accounts.GetList();
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<StudentGetDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }
        // GET: api/Accounts/Student
        [Tags(Actor.Test)]
        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.False}] Get all the student account"
      )]
        [HttpGet("Student")]
        public async Task<IActionResult> GetStudents()
        {
            IQueryable<Account> list = services.Accounts.GetList().Where(e => e.RoleId == (int)RoleNameEnum.Student);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<StudentGetDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Accounts/Student
        [Tags(Actor.Test)]
        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.False}] Get all the parent account"
        )]
        [HttpGet("Parent")]
        public async Task<IActionResult> GetParents()
        {
            IQueryable<Account> list = services.Accounts.GetList().Where(e => e.RoleId == (int)RoleNameEnum.Parent);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET: api/Accounts/5
        [Tags(Actor.Test)]
        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.False}] Get account info "
        )]
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetUser(int id)
        {
          if (context.Accounts == null)
          {
              return NotFound();
          }
            var user = await context.Accounts.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Account user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            context.Entry(user).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostUser(Account user)
        {
          if (context.Accounts == null)
          {
              return Problem("Entity set 'TempContext.User'  is null.");
          }
            context.Accounts.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (context.Accounts == null)
            {
                return NotFound();
            }
            var user = await context.Accounts.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            context.Accounts.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        ////code yt
        //public async Task<AppUser> GetUserByIdAsync(Guid id)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetUserByIdAsync(id)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/Room: GetRoomById(id)");
        //    return await dbContext.Users.FindAsync(id);
        //}

        //public async Task<AppUser> GetUserByUsernameAsync(string username)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetUserByUsernameAsync(username)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetUserByUsernameAsync(username)");
        //    return await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == username);
        //}

        //public async Task<MemberDto> GetMemberAsync(string username)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetMemberAsync(username)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetMemberAsync(username)");
        //    return await dbContext.Users.Where(x => x.UserName == username)
        //        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)//add CreateMap<AppUser, MemberDto>(); in AutoMapperProfiles
        //        .SingleOrDefaultAsync();
        //}

        //public async Task<IEnumerable<MemberDto>> GetUsersOnlineAsync(UserConnectionDto[] userOnlines)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetUsersOnlineAsync(UserConnectionDto[])");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetUsersOnlineAsync(UserConnectionDto[])");
        //    var listUserOnline = new List<MemberDto>();
        //    foreach (var u in userOnlines)
        //    {
        //        var user = await dbContext.Users.Where(x => x.UserName == u.UserName)
        //        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        //        .SingleOrDefaultAsync();

        //        listUserOnline.Add(user);
        //    }
        //    //return await Task.Run(() => listUserOnline.ToList());
        //    return await Task.FromResult(listUserOnline.ToList());
        //}

        //public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetMembersAsync(UserParams)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetMembersAsync(UserParams)");
        //    var query = dbContext.Users.AsQueryable();
        //    query = query.Where(u => u.UserName != userParams.CurrentUsername).OrderByDescending(u => u.LastActive);

        //    return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        //}

        //public async Task<IEnumerable<MemberDto>> SearchMemberAsync(string displayname)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: SearchMemberAsync(name)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: SearchMemberAsync(name)");
        //    return await dbContext.Users.Where(u => u.DisplayName.ToLower().Contains(displayname.ToLower()))
        //        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        //        .ToListAsync();
        //}

        //public async Task<AppUser> UpdateLocked(string username)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: UpdateLocked(username)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: UpdateLocked(username)");
        //    var user = await dbContext.Users.SingleOrDefaultAsync(x => x.UserName == username);
        //    if (user != null)
        //    {
        //        user.Locked = !user.Locked;
        //    }
        //    return user;
        //}
    }
}
