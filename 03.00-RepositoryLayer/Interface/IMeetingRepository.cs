using DataLayer.DBObject;

namespace RepositoryLayer.Interface
{
    public interface IMeetingRepository : IBaseRepo<Meeting, int>
    {
        Task<Meeting> GetMeetingForConnection(string connectionId);
    }
}