using APIExtension.ClaimsPrinciple;
using APIExtension.Const;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.ClassImplement;
using RepositoryLayer.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IRepoWrapper repos;

        public StatsController(IRepoWrapper repos)
        {
            this.repos = repos;
        }

        //Get: api/Accounts/search
        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Search students by id, username, mail, Full Name"
            , Description = "Search theo tên, username, id" +
            "<br>Để search thêm thành viên mới cho group, thêm groupId để loại ra hết những student đã liên quan đến nhóm"
        )]
        [HttpGet("{studentId}/{month}")]
        [Authorize(Roles = Actor.Student_Parent)]
        public IActionResult GetStatForStudentInMonth(int studentId, DateTime month)
        {
            if (HttpContext.User.IsInRole(Actor.Student) && HttpContext.User.GetUserId() != studentId)
            {
                return Unauthorized("Bạn không thể xem dữ liệu của học sinh khác");
            }
            DateTime start = new DateTime(month.Year, month.Month, 1);
            DateTime end = start.AddMonths(1);
            //Nếu tháng này thì chỉ lấy past meeting
            IQueryable<Meeting> allMeetingsOfJoinedGroups = month.Month == DateTime.Now.Month
                ? repos.Meetings.GetList()
                .Include(c => c.Connections)
                .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
                .Where(e => e.Start >= start && e.Start.Value.Date < end
                    && e.Group.GroupMembers.Any(gm => gm.AccountId == studentId)
                    //lấy past meeting
                    && (e.End != null || e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today))
                : repos.Meetings.GetList()
                .Include(c => c.Connections)
                .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
                .Where(c => c.Start>=start && c.Start.Value.Date<end
                    && c.Group.GroupMembers.Any(gm => gm.AccountId == studentId));
            int totalMeetings = allMeetingsOfJoinedGroups.Count()==0 
                ? 0 : allMeetingsOfJoinedGroups.Count();
            int atendedMeetings = allMeetingsOfJoinedGroups.Count() == 0 
                ? 0 : allMeetingsOfJoinedGroups
                .Where(e => e.Connections.Any(c=>c.AccountId == studentId)).Count();
            var totalMeetingTime = allMeetingsOfJoinedGroups.Count() == 0 ? 0
                : allMeetingsOfJoinedGroups.SelectMany(m => m.Connections).Select(e => e.End.Value - e.Start).Select(ts => ts.Ticks).Sum();
            //var totalMeetingTime = allMeetingsOfJoinedGroups.SelectMany(m => m.Connections);//.Select(e=>e.End.Value-e.Start).Select(ts=>ts.Ticks).Sum(); 
            return Ok(new
            {
                TotalMeetings = totalMeetings,
                AtendedMeetings = atendedMeetings,
                MissedMeetings = totalMeetings - atendedMeetings,
                TotalMeetingTme = totalMeetingTime == 0 ? "Chưa tham gia buổi học nào" 
                    : new TimeSpan(totalMeetingTime).ToString("HH:mm:ss"),
            });
        }
    }
}
