using DataLayer.DBObject;
using ShareResource;
using ShareResource.DTO;
using ShareResource.FilterParams;

namespace RepositoryLayer.Interface
{
    public interface IMeetingRoomRepository
    {
        Task<MeetingRoom> GetRoomById(int roomId);
        //Task<Meeting> GetMeetingForConnection(string connectionId);
        void RemoveConnection(Connection connection);
        void AddRoom(MeetingRoom room);
        Task<MeetingRoom> DeleteRoom(int id);
        Task<MeetingRoom> EditRoom(int id, string newName);
        //Task DeleteAllRoom();
        Task<PagedList<RoomDto>> GetAllRoomAsync(RoomParams roomParams);
        Task<RoomDto> GetRoomDtoById(int roomId);
        Task UpdateCountMember(int roomId, int count);
        Task<MeetingRoom> GetRoomForMeetingAsync(int meetingIdInt);
    }
}