using AutoMapper;
using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ShareResource;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using ShareResource.DTO.Room;
using AutoMapper.QueryableExtensions;
using ShareResource.FilterParams;

namespace RepositoryLayer.ClassImplement
{
    public class MeetingRoomRepository : BaseRepo<MeetingRoom, int>, IMeetingRoomRepository
    {
        private readonly GroupStudyContext _context;
        private readonly IMapper _mapper;

        public MeetingRoomRepository(GroupStudyContext context, IMapper mapper)
            : base(context)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: ctor()");
            _mapper = mapper;
        }

        public async Task<MeetingRoom> GetRoomById(int roomId)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetMeetingRoomById(id)");
            return await _context.MeetingRooms.Include(e => e.Meetings).ThenInclude(x => x.Connections).FirstOrDefaultAsync(x => x.Id == roomId);
        }

        public async Task<RoomDto> GetRoomDtoById(int roomId)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetRoomDtoById(id)");
            return await _context.MeetingRooms.Where(r => r.Id == roomId).ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }

        

        public void RemoveConnection(Connection connection)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: RemoveConnection(Connection)");
            _context.Connections.Remove(connection);
            _context.SaveChanges();
        }

        public void AddRoom(MeetingRoom room)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: AddMeetingRoom(MeetingRoom)");
            _context.MeetingRooms.Add(room);
        }

        /// <summary>
        /// return null no action to del else delete thanh cong
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MeetingRoom> DeleteRoom(int id)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: DeleteMeetingRoom(id)");
            var room = await _context.MeetingRooms.FindAsync(id);
            if (room != null)
            {
                _context.MeetingRooms.Remove(room);
            }
            return room;
        }

        public async Task<MeetingRoom> EditRoom(int id, string newName)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: EditMeetingRoom(id, name)");
            var room = await _context.MeetingRooms.FindAsync(id);
            if (room != null)
            {
                room.Name = newName;
            }
            await _context.SaveChangesAsync();
            return room;
        }

        //public async Task DeleteAllMeetingRoom()
        //{
        //    //Console.WriteLine("4.         " + new String('~', 50));
        //    //Console.WriteLine("4.         Repo/MeetingRoom: DeleteAllMeetingRoom()");
        //    var list = await _context.MeetingRooms.ToListAsync();
        //    _context.RemoveRange(list);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<PagedList<RoomDto>> GetAllRoomAsync(RoomParams roomParams)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: GetAllMeetingRoomAsync(MeetingRoomParams)");
            var list = _context.MeetingRooms.AsQueryable();
            //using AutoMapper.QueryableExtensions; list.ProjectTo<RoomDto>
            return await PagedList<RoomDto>.CreateAsync(list.ProjectTo<RoomDto>(_mapper.ConfigurationProvider).AsNoTracking(), roomParams.PageNumber, roomParams.PageSize);
        }

        public async Task UpdateCountMember(int roomId, int count)
        {
            //Console.WriteLine("4.         " + new String('~', 50));
            //Console.WriteLine("4.         Repo/MeetingRoom: UpdateCountMember(id, count)");
            var room = await _context.MeetingRooms.FindAsync(roomId);
            if (room != null)
            {
                room.CountMember = count;
            }
            await _context.SaveChangesAsync();

        }

        public async Task<MeetingRoom> GetRoomForMeetingAsync(int meetingIdInt)
        {
            return (await _context.Meetings
                .Include(e=>e.MeetingRoom)
                .SingleOrDefaultAsync(e=>e.Id==meetingIdInt))
                .MeetingRoom;
        }
    }
}