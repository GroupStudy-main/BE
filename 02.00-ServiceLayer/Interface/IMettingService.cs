using DataLayer.DBObject;
using ShareResource.DTO;

namespace ServiceLayer.Interface
{
    public interface IMettingService
    {
        public Task<bool> AnyAsync(int id);
        public IQueryable<PastMeetingGetDto> GetPastMeetingsForGroup(int groupId);
        public IQueryable<ScheduleMeetingGetDto> GetScheduleMeetingsForGroup(int groupId);
        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForGroup(int groupId);
    }
}