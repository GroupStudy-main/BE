using DataLayer.DBContext;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class MeetingRepository : BaseRepo<Meeting, int>, IMeetingRepository 
    {
        public MeetingRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public async Task<Meeting> GetMeetingForConnection(string connectionId)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetMeetingRoomForConnection(connectionId)");
            return (await dbContext.Connections
                .Include(x => x.Meeting)
                .SingleOrDefaultAsync(x => x.Id == connectionId))
                .Meeting;
        }
    }
}