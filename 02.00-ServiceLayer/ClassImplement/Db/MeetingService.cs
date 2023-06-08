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
    
    public IQueryable<Meeting> GetListMeeting(string type)
    {
        IQueryable<Meeting> result = null;
        if ("Scheduled".Equals(type))
        {
            result = repos.Meetings.GetList().Where(meeting =>
                meeting.Start == null && meeting.ScheduleStart >= DateTime.Today);
        }
        if ("History".Equals(type))
        {
            result = repos.Meetings.GetList().Where(meeting =>
                meeting.End != null || meeting.ScheduleEnd < DateTime.Today);
        }
        if ("Live".Equals(type))
        {
            result = repos.Meetings.GetList().Where(meeting =>
                meeting.Start != null && meeting.End == null);
        }
        if ("All".Equals(type))
        {
            result = repos.Meetings.GetList();
        }
        return result;
    }

    public async Task CreateMeeting(Meeting meeting)
    {
        int count = 0;
        // requirement chỗ này thế nào? CountMember nằm trong Group chứ sao lại nằm trong meeting
        var countMember = await repos.Groups.GetByIdAsync(meeting.GroupId);
        if (countMember != null && countMember.GroupMembers != null)
        {
            count = countMember.GroupMembers.Count;
        }
        meeting.CountMember = count;
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