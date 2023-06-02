using AutoMapper;
using DataLayer.DBContext;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using ShareResource.FilterParams;
using ShareResource;
using AutoMapper.QueryableExtensions;

namespace RepositoryLayer.ClassImplement
{
    internal class MeetingRepository : BaseRepo<Meeting, int>, IMeetingRepository 
    {
        private readonly GroupStudyContext dbContext;
        private IMapper _mapper;

        public MeetingRepository(GroupStudyContext context, IMapper mapper)
            : base(context)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: ctor()");
            _mapper = mapper;
        }

        public async Task<Meeting> GetMeetingById(int roomId)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetMeetingRoomById(id)");
            return await dbContext.Meetings.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Id == roomId);
        }

        public async Task<MeetingDto> GetRoomDtoById(int roomId)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetRoomDtoById(id)");
            return await dbContext.Meetings.Where(r => r.Id == roomId).ProjectTo<MeetingDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }



        public void RemoveConnection(Connection connection)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: RemoveConnection(Connection)");
            dbContext.Connections.Remove(connection);
            dbContext.SaveChanges();
        }

        public void AddMeeting(Meeting meeting)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: AddMeetingRoom(MeetingRoom)");
            dbContext.Meetings.Add(meeting);
        }

        /// <summary>
        /// return null no action to del else delete thanh cong
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Meeting> DeleteMeeting(int id)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: DeleteMeetingRoom(id)");
            var room = await dbContext.Meetings.FindAsync(id);
            if (room != null)
            {
                dbContext.Meetings.Remove(room);
            }
            return room;
        }

        public async Task<Meeting> EditMeeting(int id, string newName)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: EditMeetingRoom(id, name)");
            var room = await dbContext.Meetings.FindAsync(id);
            if (room != null)
            {
                room.Name = newName;
            }
            await dbContext.SaveChangesAsync();
            return room;
        }

        //public async Task DeleteAllMeetingRoom()
        //{
        //    //Console.WriteLine("4.         " + new String('~', 50));
        //    //Console.WriteLine("4.         Repo/MeetingRoom: DeleteAllMeetingRoom()");
        //    var list = await dbContext.Rooms.ToListAsync();
        //    dbContext.RemoveRange(list);
        //    await dbContext.SaveChangesAsync();
        //}

        public async Task<PagedList<MeetingDto>> GetAllRoomAsync(RoomParams roomParams)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetAllMeetingRoomAsync(MeetingRoomParams)");
            var list = dbContext.Meetings.AsQueryable();
            //using AutoMapper.QueryableExtensions; list.ProjectTo<RoomDto>
            return await PagedList<MeetingDto>.CreateAsync(list.ProjectTo<MeetingDto>(_mapper.ConfigurationProvider).AsNoTracking(), roomParams.PageNumber, roomParams.PageSize);
        }

        public async Task UpdateCountMember(int roomId, int count)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: UpdateCountMember(id, count)");
            var room = await dbContext.Meetings.FindAsync(roomId);
            if (room != null)
            {
                room.CountMember = count;
            }
            await dbContext.SaveChangesAsync();

        }

        public async Task<Meeting> GetRoomForMeetingAsync(int meetingIdInt)
        {
            return await dbContext.Meetings
                .SingleOrDefaultAsync(e => e.Id == meetingIdInt);
        }

        public override Task CreateAsync(Meeting entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Meeting> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<Meeting> GetList()
        {
            return base.GetList();
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

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Meeting entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}