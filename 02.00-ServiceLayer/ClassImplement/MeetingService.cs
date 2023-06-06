using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using ShareResource.DTO;

namespace ServiceLayer.ClassImplement
{
    internal class MeetingService : IMettingService
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
            return await repos.Meetings.GetList().AnyAsync(e=>e.Id == id);
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

        public async Task<Meeting> GetByIdAsync(int id)
        {
            return await repos.Meetings.GetByIdAsync(id);
        }

        public IQueryable<PastMeetingGetDto> GetPastMeetingsForGroup(int groupId)
        {
            return repos.Meetings.GetList()
               //.Where(e => e.GroupId == groupId && (e.End != null || (e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today && e.Start == null)))
               .Where(e => e.GroupId == groupId && (e.End != null || (e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today)))
               .ProjectTo<PastMeetingGetDto>(mapper.ConfigurationProvider);
        }

        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForGroup(int groupId)
        {
            //var liveMeetings = repos.Meetings.GetList()
            //    .Where(e => e.Start != null && e.End == null);
            return repos.Meetings.GetList()
                .Where(e => e.GroupId==groupId && e.Start != null && e.End == null)
                .ProjectTo<LiveMeetingGetDto>(mapper.ConfigurationProvider);
        }

        public IQueryable<ScheduleMeetingGetDto> GetScheduleMeetingsForGroup(int groupId)
        {
            return repos.Meetings.GetList()
                //.Where(e => e.GroupId == groupId && (e.End != null || (e.ScheduleStart != null && e.ScheduleStart.Value.Date >= DateTime.Today)))
                .Where(e => e.GroupId == groupId && (e.ScheduleStart != null && e.ScheduleStart.Value.Date >= DateTime.Today && e.Start == null))
                .ProjectTo<ScheduleMeetingGetDto>(mapper.ConfigurationProvider);
        }

        public Task UpdateScheduleMeetingAsync(ScheduleMeetingUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}