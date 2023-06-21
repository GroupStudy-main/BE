using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.DTO;
using ShareResource.UpdateApiExtension;

namespace ServiceLayer.ClassImplement.Db
{
    internal class MeetingService : IMeetingService
    {
        private IRepoWrapper repos;
        private IMapper mapper;

        public MeetingService(IRepoWrapper repos, IMapper mapper)
        {
            this.repos = repos;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await repos.Meetings.GetList().AnyAsync(e => e.Id == id);
        }

        public async Task CreateInstantMeetingAsync(InstantMeetingCreateDto dto)
        {
            Meeting meeting = mapper.Map<Meeting>(dto);
            await repos.Meetings.CreateAsync(meeting);
        }

        public async Task CreateScheduleMeetingAsync(ScheduleMeetingCreateDto dto)
        {
            Meeting meeting = mapper.Map<Meeting>(dto);
            await repos.Meetings.CreateAsync(meeting);
        }

        public async Task<IEnumerable<Meeting>> MassCreateScheduleMeetingAsync(ScheduleMeetingMassCreateDto dto)
        {
            DateTime[] dates = Enumerable.Range(0, 1 + dto.ScheduleRangeEnd.Subtract(dto.ScheduleSRangeStart).Days)
                .Select(offset => dto.ScheduleSRangeStart.AddDays(offset))
                .Where(date => dto.DayOfWeeks.Contains(date.DayOfWeek + 1))
                .ToArray();
            IEnumerable<Meeting> creatingMeetings = dates.Select(date => new Meeting
            {
                Name = dto.Name + " " + date.ToString("d/M"),
                GroupId = dto.GroupId,
                ScheduleStart = date.Add(dto.ScheduleStartTime),
                ScheduleEnd = date.Add(dto.ScheduleEndTime),
            });
            return await repos.Meetings.MassCreateAsync(creatingMeetings);
        }

        public async Task<Meeting> GetByIdAsync(int id)
        {
            return await repos.Meetings.GetByIdAsync(id);
        }

        public IQueryable<PastMeetingGetDto> GetPastMeetingsForGroup(int groupId)
        {
            return repos.Meetings.GetList()
               //.Where(e => e.GroupId == groupId && (e.End != null || (e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today && e.Start == null)))
               .Where(e => e.GroupId == groupId && (e.End != null || e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today))
               .ProjectTo<PastMeetingGetDto>(mapper.ConfigurationProvider);
        }

        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForGroup(int groupId)
        {
            //var liveMeetings = repos.Meetings.GetList()
            //    .Where(e => e.Start != null && e.End == null);
            return repos.Meetings.GetList()
                .Where(e => e.GroupId == groupId && e.Start != null && e.End == null)
                .ProjectTo<LiveMeetingGetDto>(mapper.ConfigurationProvider);
        }

        public IQueryable<ScheduleMeetingGetDto> GetScheduleMeetingsForGroup(int groupId)
        {
            return repos.Meetings.GetList()
                //.Where(e => e.GroupId == groupId && (e.End != null || (e.ScheduleStart != null && e.ScheduleStart.Value.Date >= DateTime.Today)))
                .Where(e => e.GroupId == groupId && e.ScheduleStart != null && e.ScheduleStart.Value.Date >= DateTime.Today && e.Start == null)
                .ProjectTo<ScheduleMeetingGetDto>(mapper.ConfigurationProvider);
        }

        public async Task UpdateScheduleMeetingAsync(ScheduleMeetingUpdateDto dto)
        {
            Meeting existed = await repos.Meetings.GetByIdAsync(dto.Id);
            Meeting updated = new Meeting
            {
                Id = dto.Id,
                GroupId = existed.GroupId,
                Name = dto.Name,
                ScheduleStart = dto.Date.Date.Add(dto.ScheduleStartTime),
                ScheduleEnd = dto.Date.Date.Add(dto.ScheduleEndTime),
            };
            existed.PatchUpdate(updated);
            await repos.Meetings.UpdateAsync(existed);
        }

        public async Task StartScheduleMeetingAsync(Meeting meeting)
        {
            await repos.Meetings.UpdateAsync(meeting);
        }

        public async Task DeleteScheduleMeetingAsync(Meeting meeting)
        {
            await repos.Meetings.RemoveAsync(meeting.Id);
        }
    }
}