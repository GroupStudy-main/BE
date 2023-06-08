using DataLayer.DBObject;

namespace ServiceLayer.Interface.Db;

public interface IMeetingService
{
    IQueryable<Meeting> GetListMeeting(string type);

    Task CreateMeeting(Meeting meeting);
    
    Task UpdateMeeting(Meeting meeting);
    
    Task<Meeting> GetById(int id);
    
}