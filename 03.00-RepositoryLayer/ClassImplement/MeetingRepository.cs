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
            _mapper = mapper;
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
        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Meeting entity)
        {
            return base.UpdateAsync(entity);
        }

        ///SignalR
        ////////////////////////////////////////////////////////////
        public Task<Meeting> GetMeetingByIdSignalr(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<Meeting> GetMeetingForConnectionSignalr(string connectionId)
        {
            throw new NotImplementedException();
        }

        public void EndConnectionSignalr(Connection connection)
        {
            throw new NotImplementedException();
        }

        

    }
}