using Microsoft.AspNetCore.Mvc;
using DataLayer.DBObject;
using API.SignalRHub.Tracker;
using API.SignalRHub;
using Microsoft.AspNetCore.SignalR;
using RepositoryLayer.Interface;
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
using ShareResource.DTO.Account;

namespace API.Controllers
{
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private const string FAIL_CONFIRM_PASSWORD_MSG = "Fail to confirm password";
        private readonly IServiceWrapper services;
        private readonly IRepoWrapper unitOfWork;
        private readonly IHubContext<PresenceHub> presenceHub;
        private readonly PresenceTracker presenceTracker;
        private readonly IMapper mapper;

        public AccountsController(IServiceWrapper services, IRepoWrapper unitOfWork, IHubContext<PresenceHub> presenceHub, PresenceTracker presenceTracker, IMapper mapper)
        {
            this.services = services;
            this.unitOfWork = unitOfWork;
            this.presenceHub = presenceHub;
            this.presenceTracker = presenceTracker;
            this.mapper = mapper;
        }

        //Get: api/Accounts/search
        [SwaggerOperation(
         Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Search students by id, username, mail, Full Name"
       )]
        [Authorize(Roles = Actor.Student_Parent)]
        [HttpGet("search")]
        public async Task<IActionResult> SearchStudent(string search)
        {
            var list = services.Accounts.SearchStudents(search);

            if (list == null)
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<AccountProfileDto>(mapper.ConfigurationProvider);
            return Ok(list);
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
            return Ok(user);
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
                return Unauthorized("You can't update other's profile");
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
          Summary = $"[{Actor.Student_Parent}/{Finnished.No_Test}/{Auth.True}] Update account password"
        )]
        [Authorize(Roles = Actor.Student_Parent)]
        [HttpPut("{id}/Password")]
        public async Task<IActionResult> ChangePassword(int id, AccountChangePasswordDto dto)
        {
            if (id != HttpContext.User.GetUserId())
            {
                return Unauthorized("You can't update other's profile");
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
        [SwaggerOperation(
            Summary = $"[{Actor.Test}/{Finnished.False}] Get all the account"
        )]
        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAccount()
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
        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.False}] Get account info"
        )]
        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await services.Accounts.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var mapped = mapper.Map<StudentGetDto>(user);
            return Ok(mapped);
        }

        //// DELETE: api/Accounts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    if (services.Accounts == null)
        //    {
        //        return NotFound();
        //    }
        //    var dto = await services.Accounts.FindAsync(id);
        //    if (dto == null)
        //    {
        //        return NotFound();
        //    }

        //    services.Accounts.Remove(dto);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool UserExists(int id)
        {
            //return (services.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
            return (services.Accounts?.GetByIdAsync(id) is not null);
        }

        ////code yt
        //public async Task<AppUser> GetUserByIdAsync(Guid id)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetUserByIdAsync(id)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/Room: GetRoomById(id)");
        //    return await services.Users.FindAsync(id);
        //}

        //public async Task<AppUser> GetUserByUsernameAsync(string username)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetUserByUsernameAsync(username)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetUserByUsernameAsync(username)");
        //    return await services.Users.SingleOrDefaultAsync(u => u.UserName == username);
        //}

        //public async Task<MemberDto> GetMemberAsync(string username)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetMemberAsync(username)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetMemberAsync(username)");
        //    return await services.Users.Where(x => x.UserName == username)
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
        //        var dto = await services.Users.Where(x => x.UserName == u.UserName)
        //        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        //        .SingleOrDefaultAsync();

        //        listUserOnline.Add(dto);
        //    }
        //    //return await Task.Run(() => listUserOnline.ToList());
        //    return await Task.FromResult(listUserOnline.ToList());
        //}

        //public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: GetMembersAsync(UserParams)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: GetMembersAsync(UserParams)");
        //    var query = services.Users.AsQueryable();
        //    query = query.Where(u => u.UserName != userParams.CurrentUsername).OrderByDescending(u => u.LastActive);

        //    return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        //}

        //public async Task<IEnumerable<MemberDto>> SearchMemberAsync(string displayname)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: SearchMemberAsync(name)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: SearchMemberAsync(name)");
        //    return await services.Users.Where(u => u.DisplayName.ToLower().Contains(displayname.ToLower()))
        //        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        //        .ToListAsync();
        //}

        //public async Task<AppUser> UpdateLocked(string username)
        //{
        //    Console.WriteLine("4.         " + new String('~', 50));
        //    Console.WriteLine("4.         Repo/User: UpdateLocked(username)");
        //    FunctionTracker.Instance().AddRepoFunc("Repo/User: UpdateLocked(username)");
        //    var dto = await services.Users.SingleOrDefaultAsync(x => x.UserName == username);
        //    if (dto != null)
        //    {
        //        dto.Locked = !dto.Locked;
        //    }
        //    return dto;
        //}

    }

}
