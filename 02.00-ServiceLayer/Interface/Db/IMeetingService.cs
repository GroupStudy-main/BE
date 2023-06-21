using DataLayer.DBObject;
using ShareResource.DTO;

namespace ServiceLayer.Interface.Db
{
    public interface IMeetingService
    {
        public Task<bool> AnyAsync(int id);
        public IQueryable<PastMeetingGetDto> GetPastMeetingsForGroup(int groupId);
        public IQueryable<ScheduleMeetingGetDto> GetScheduleMeetingsForGroup(int groupId);
        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForGroup(int groupId);
        public Task<Meeting> GetByIdAsync(int id);
        public Task CreateScheduleMeetingAsync(ScheduleMeetingCreateDto dto);
        public Task<IEnumerable<Meeting>> MassCreateScheduleMeetingAsync(ScheduleMeetingMassCreateDto dto);
        public Task CreateInstantMeetingAsync(InstantMeetingCreateDto dto);
        public Task UpdateScheduleMeetingAsync(ScheduleMeetingUpdateDto dto);
        public Task StartScheduleMeetingAsync(Meeting meeting);
        public Task DeleteScheduleMeetingAsync(Meeting meeting);
    }
}