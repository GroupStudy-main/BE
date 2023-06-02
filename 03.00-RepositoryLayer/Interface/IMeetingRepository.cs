using DataLayer.DBObject;
using ShareResource.DTO;
using ShareResource.FilterParams;
using ShareResource;

namespace RepositoryLayer.Interface
{
    public interface IMeetingRepository : IBaseRepo<Meeting, int>
    {
        Task<Meeting> GetMeetingForConnection(string connectionId);
        Task<Meeting> GetMeetingById(int roomId);
        //Task<Meeting> GetMeetingForConnection(string connectionId);
        void RemoveConnection(Connection connection);
        void AddMeeting(Meeting meeting);
        Task<Meeting> DeleteMeeting(int id);
        Task<Meeting> EditMeeting(int id, string newName);
        //Task DeleteAllRoom();
        Task<PagedList<MeetingDto>> GetAllRoomAsync(RoomParams roomParams);
        Task<MeetingDto> GetRoomDtoById(int roomId);
        Task UpdateCountMember(int roomId, int count);
        Task<Meeting> GetRoomForMeetingAsync(int meetingIdInt);
    }
}