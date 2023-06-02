using AutoMapper;
using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ShareResource;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using ShareResource.DTO;
using AutoMapper.QueryableExtensions;
using ShareResource.FilterParams;

namespace RepositoryLayer.ClassImplement
{
    //public class MeetingRepository : BaseRepo<Meeting, int>, IMeetingRepository
    //{
    //    private readonly GroupStudyContext _context;
    //    private readonly IMapper _mapper;

    //    public MeetingRepository(GroupStudyContext context, IMapper mapper)
    //        : base(context)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: ctor()");
    //        _mapper = mapper;
    //    }

    //    public async Task<Meeting> GetMeetingById(int roomId)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: GetMeetingRoomById(id)");
    //        return await _context.Meetings.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Id == roomId);
    //    }

    //    public async Task<MeetingDto> GetRoomDtoById(int roomId)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: GetRoomDtoById(id)");
    //        return await _context.Meetings.Where(r => r.Id == roomId).ProjectTo<MeetingDto>(_mapper.ConfigurationProvider)
    //            .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
    //    }

        

    //    public void RemoveConnection(Connection connection)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: RemoveConnection(Connection)");
    //        _context.Connections.Remove(connection);
    //        _context.SaveChanges();
    //    }

    //    public void AddMeeting(Meeting meeting)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: AddMeetingRoom(MeetingRoom)");
    //        _context.Meetings.Add(meeting);
    //    }

    //    /// <summary>
    //    /// return null no action to del else delete thanh cong
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    public async Task<Meeting> DeleteMeeting(int id)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: DeleteMeetingRoom(id)");
    //        var room = await _context.Meetings.FindAsync(id);
    //        if (room != null)
    //        {
    //            _context.Meetings.Remove(room);
    //        }
    //        return room;
    //    }

    //    public async Task<Meeting> EditMeeting(int id, string newName)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: EditMeetingRoom(id, name)");
    //        var room = await _context.Meetings.FindAsync(id);
    //        if (room != null)
    //        {
    //            room.Name = newName;
    //        }
    //        await _context.SaveChangesAsync();
    //        return room;
    //    }

    //    //public async Task DeleteAllMeetingRoom()
    //    //{
    //    //    //Console.WriteLine("4.         " + new String('~', 50));
    //    //    //Console.WriteLine("4.         Repo/MeetingRoom: DeleteAllMeetingRoom()");
    //    //    var list = await _context.Rooms.ToListAsync();
    //    //    _context.RemoveRange(list);
    //    //    await _context.SaveChangesAsync();
    //    //}

    //    public async Task<PagedList<MeetingDto>> GetAllRoomAsync(RoomParams roomParams)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: GetAllMeetingRoomAsync(MeetingRoomParams)");
    //        var list = _context.Meetings.AsQueryable();
    //        //using AutoMapper.QueryableExtensions; list.ProjectTo<RoomDto>
    //        return await PagedList<MeetingDto>.CreateAsync(list.ProjectTo<MeetingDto>(_mapper.ConfigurationProvider).AsNoTracking(), roomParams.PageNumber, roomParams.PageSize);
    //    }

    //    public async Task UpdateCountMember(int roomId, int count)
    //    {
    //        //Console.WriteLine("4.         " + new String('~', 50));
    //        //Console.WriteLine("4.         Repo/MeetingRoom: UpdateCountMember(id, count)");
    //        var room = await _context.Meetings.FindAsync(roomId);
    //        if (room != null)
    //        {
    //            room.CountMember = count;
    //        }
    //        await _context.SaveChangesAsync();

    //    }

    //    public async Task<Meeting> GetRoomForMeetingAsync(int meetingIdInt)
    //    {
    //        return await _context.Meetings
    //            .SingleOrDefaultAsync(e=>e.Id==meetingIdInt);
    //    }
    ////}
}