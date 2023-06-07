using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.ClassImplement.Db;

public class MeetingService : IMeetingService
{
    private IRepoWrapper repos;

    public MeetingService(IRepoWrapper repos)
    {
        this.repos = repos;
    }
    
    public IQueryable<Meeting> GetListMeeting()
    {
        return repos.Meetings.GetList();
    }

    public async Task CreateMeeting(Meeting meeting)
    {
        // requirement chỗ này thế nào? CountMember nằm trong Group chứ sao lại nằm trong meeting
        meeting.CountMember = 4;
        repos.Meetings.Create(meeting);
    }

    public async Task UpdateMeeting(Meeting meeting)
    {
        await repos.Meetings.UpdateAsync(meeting);
    }

    public Task<Meeting> GetById(int id)
    {
        return repos.Meetings.GetByIdAsync(id);
    }
}