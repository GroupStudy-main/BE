using DataLayer.DBObject;

namespace ServiceLayer.Interface.Db;

public interface IMeetingService
{
    IQueryable<Meeting> GetListMeeting();

    Task CreateMeeting(Meeting meeting);
    
    Task UpdateMeeting(Meeting meeting);
    
    Task<Meeting> GetById(int id);

}