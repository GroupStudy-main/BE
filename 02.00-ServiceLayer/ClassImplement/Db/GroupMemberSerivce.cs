using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.DTO;
using ShareResource.Enums;

namespace ServiceLayer.ClassImplement.Db
{
    internal class GroupMemberSerivce : IGroupMemberSerivce
    {
        private IRepoWrapper repos;
        private IMapper mapper;

        public GroupMemberSerivce(IRepoWrapper repos, IMapper mapper)
        {
            this.repos = repos;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await repos.GroupMembers.GetList().AnyAsync(x => x.Id == id);
        }

        public async Task<GroupMember> GetByIdAsync(int inviteId)
        {
            return await repos.GroupMembers.GetList().FirstOrDefaultAsync(x => x.Id == inviteId);
        }

        public IQueryable<AccountProfileDto> GetMembersJoinForGroup(int groupId)
        {
            IQueryable<Account> list = repos.GroupMembers.GetList()
                .Where(e => e.GroupId == groupId && (e.State == GroupMemberState.Member || e.State == GroupMemberState.Leader))
                .Include(e => e.Account)
                .Select(e => e.Account);
            return list.ProjectTo<AccountProfileDto>(mapper.ConfigurationProvider);
        }

        public IQueryable<GroupMemberRequestGetDto> GetJoinRequestForGroup(int groupId)
        {
            IQueryable<GroupMember> list = repos.GroupMembers.GetList()
                .Where(e => e.GroupId == groupId && e.State == GroupMemberState.Requesting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<GroupMemberRequestGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<GroupMemberInviteGetDto> GetJoinInviteForGroup(int groupId)
        {
            IQueryable<GroupMember> list = repos.GroupMembers.GetList()
                .Where(e => e.GroupId == groupId && e.State == GroupMemberState.Inviting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<GroupMemberInviteGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<GroupMemberRequestGetDto> GetJoinRequestForStudent(int studentId)
        {
            IQueryable<GroupMember> list = repos.GroupMembers.GetList()
                .Where(e => e.AccountId == studentId && e.State == GroupMemberState.Requesting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<GroupMemberRequestGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<GroupMemberInviteGetDto> GetJoinInviteForStudent(int studentId)
        {
            IQueryable<GroupMember> list = repos.GroupMembers.GetList()
                .Where(e => e.AccountId == studentId && e.State == GroupMemberState.Inviting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<GroupMemberInviteGetDto>(mapper.ConfigurationProvider);
        }

        public async Task CreateJoinInvite(GroupMemberInviteCreateDto dto)
        {
            GroupMember invite = mapper.Map<GroupMember>(dto);
            await repos.GroupMembers.CreateAsync(invite);
        }

        public async Task CreateJoinRequest(GroupMemberRequestCreateDto dto)
        {
            GroupMember request = mapper.Map<GroupMember>(dto);
            await repos.GroupMembers.CreateAsync(request);
        }

        public async Task<GroupMember> GetGroupMemberOfStudentAndGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
               .SingleOrDefaultAsync(e => e.AccountId == studentId && e.GroupId == groupId);
        }

        public async Task AcceptOrDeclineInviteAsync(GroupMember existed, bool isAccepted)
        {
            if (existed.State != GroupMemberState.Inviting)
            {
                throw new Exception("Đây không phải là thư mời");
            }
            existed.State = isAccepted ? GroupMemberState.Member : GroupMemberState.Declined;
            await repos.GroupMembers.UpdateAsync(existed);
        }

        public async Task AcceptOrDeclineRequestAsync(GroupMember existed, bool isAccepted)
        {
            if (existed.State != GroupMemberState.Requesting)
            {
                throw new Exception("Đây không phải là yêu cầu");
            }
            existed.State = isAccepted ? GroupMemberState.Member : GroupMemberState.Declined;
            await repos.GroupMembers.UpdateAsync(existed);
        }
    }
}