﻿using APIExtension.ClaimsPrinciple;
using APIExtension.Const;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.ClassImplement;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IRepoWrapper repos;
        private readonly IMapper mapper;

        public StatsController(IRepoWrapper repos, IMapper mapper)
        {
            this.repos = repos;
            this.mapper = mapper;
        }

        //Get: api/Accounts/search
        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Student's stat by month"
            , Description = "lấy stat theo month" +
            "<br>month (yyyy-mm-dd): chỉ cần năm với tháng, day nhập đại"
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
            //int totalMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
            //    ? 0 : allMeetingsOfJoinedGroups.Count();
            int totalMeetingsCount = allMeetingsOfJoinedGroups.Count();
            var atendedMeetings = allMeetingsOfJoinedGroups
                .Where(e => e.Connections.Any(c => c.AccountId == studentId));
            int atendedMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
                ? 0 : allMeetingsOfJoinedGroups
                .Where(e => e.Connections.Any(c=>c.AccountId == studentId)).Count();
            long totalMeetingTime = allMeetingsOfJoinedGroups.Count() == 0 ? 0
                : allMeetingsOfJoinedGroups.SelectMany(m => m.Connections)
                    .Select(e => e.End.Value - e.Start).Select(ts => ts.Ticks).Sum();
            var timeSpan = new TimeSpan(totalMeetingTime);
            //var totalMeetingTime = allMeetingsOfJoinedGroups.SelectMany(m => m.Connections);//.Select(e=>e.End.Value-e.Start).Select(ts=>ts.Ticks).Sum(); 
            
            return Ok(new
            {
                TotalMeetings = allMeetingsOfJoinedGroups.ProjectTo<PastMeetingGetDto>(mapper.ConfigurationProvider),
                TotalMeetingsCount = totalMeetingsCount,
                AtendedMeetingsCount = atendedMeetingsCount,
                MissedMeetingsCount = totalMeetingsCount - atendedMeetingsCount,
                TotalMeetingTme = totalMeetingTime == 0 ? "Chưa tham gia buổi học nào" 
                    : $"{timeSpan.Hours} giờ {timeSpan.Minutes} phút {timeSpan.Seconds} giây"
            });
        }
    }
}
