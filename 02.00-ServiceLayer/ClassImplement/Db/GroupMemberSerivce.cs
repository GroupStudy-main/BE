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
        //Fix later
        public IQueryable<JoinRequestGetDto> GetJoinRequestForGroup(int groupId)
        {
            IQueryable<JoinRequest> list = repos.Requests.GetList()
                .Where(e => e.GroupId == groupId && e.State == InviteRequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            //Console.WriteLine("+_+_+_+_+_+_ " + list.Count());
            return list.ProjectTo<JoinRequestGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<JoinInviteGetDto> GetJoinInviteForGroup(int groupId)
        {
            IQueryable<JoinInvite> list = repos.Invites.GetList()
                .Where(e => e.GroupId == groupId && e.State == InviteRequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<JoinInviteGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<JoinRequestGetDto> GetJoinRequestForStudent(int studentId)
        {
            IQueryable<JoinRequest> list = repos.Requests.GetList()
                .Where(e => e.AccountId == studentId && e.State == InviteRequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            IQueryable<JoinRequestGetDto> mapped = list.ProjectTo<JoinRequestGetDto>(mapper.ConfigurationProvider);
            return mapped;
        }

        public async Task<JoinInvite> GetInviteOfStudentAndGroupAsync(int accountId, int groupId)
        {
            JoinInvite invite = await repos.Invites.GetList()
                .Include(e => e.Account)
                .Include(e => e.Group)
                .SingleOrDefaultAsync(e => e.AccountId == accountId
                    && e.GroupId == groupId && e.State == InviteRequestStateEnum.Waiting);
            return invite;

        }

        public async Task<JoinRequest> GetRequestOfStudentAndGroupAsync(int accountId, int groupId)
        {
            JoinRequest request = await repos.Requests.GetList()
                .Include(e => e.Account)
                .Include(e => e.Group)
                .SingleOrDefaultAsync(e => e.AccountId == accountId
                    && e.GroupId == groupId && e.State == InviteRequestStateEnum.Waiting);
            return request;
        }


        public IQueryable<JoinInviteGetDto> GetJoinInviteForStudent(int studentId)
        {
            IQueryable<JoinInvite> list = repos.Invites.GetList()
                .Where(e => e.AccountId == studentId && e.State == InviteRequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<JoinInviteGetDto>(mapper.ConfigurationProvider);
        }

        public async Task CreateJoinInvite(GroupMemberInviteCreateDto dto)
        {
            //GroupMember invite = mapper.Map<GroupMember>(dto);
            //await repos.GroupMembers.CreateAsync(invite);
            JoinInvite invite = mapper.Map<JoinInvite>(dto);
            await repos.Invites.CreateAsync(invite);
        }

        public async Task CreateJoinRequest(GroupMemberRequestCreateDto dto)
        {
            //GroupMember request = mapper.Map<GroupMember>(dto);
            //await repos.GroupMembers.CreateAsync(request);
            JoinRequest request = mapper.Map<JoinRequest>(dto);
            await repos.Requests.CreateAsync(request);
        }

        public async Task<GroupMember> GetGroupMemberOfStudentAndGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
               .SingleOrDefaultAsync(e => e.AccountId == studentId && e.GroupId == groupId);
        }

        public async Task AcceptOrDeclineInviteAsync(GroupMember existed, bool isAccepted)
        {
            //if (existed.State != GroupMemberState.Inviting)
            {
                throw new Exception("Đây không phải là thư mời");
            }
            existed.State = isAccepted ? GroupMemberState.Member : GroupMemberState.Banned;
            await repos.GroupMembers.UpdateAsync(existed);
        }

        public async Task AcceptOrDeclineRequestAsync(GroupMember existed, bool isAccepted)
        {
            //if (existed.State != GroupMemberState.Requesting)
            {
                throw new Exception("Đây không phải là yêu cầu");
            }
            existed.State = isAccepted ? GroupMemberState.Member : GroupMemberState.Banned;
            await repos.GroupMembers.UpdateAsync(existed);
        }
    }
}