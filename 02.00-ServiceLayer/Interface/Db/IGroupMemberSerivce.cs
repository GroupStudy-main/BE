using DataLayer.DBObject;
using ShareResource.DTO;

namespace ServiceLayer.Interface.Db
{
    public interface IGroupMemberSerivce
    {
        Task<bool> AnyAsync(int id);
        IQueryable<AccountProfileDto> GetMembersJoinForGroup(int id);
        public Task<GroupMember> GetByIdAsync(int inviteId);
        public IQueryable<JoinRequestGetDto> GetJoinRequestForGroup(int groupId);
        public IQueryable<JoinInviteGetDto> GetJoinInviteForGroup(int groupId);
        public IQueryable<JoinRequestGetDto> GetJoinRequestForStudent(int studentId);
        public IQueryable<JoinInviteGetDto> GetJoinInviteForStudent(int studentId);
        public Task CreateJoinInvite(GroupMemberInviteCreateDto dto);
        public Task CreateJoinRequest(GroupMemberRequestCreateDto dto);
        /// <summary>
        /// Get group member status object between student and group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<GroupMember> GetGroupMemberOfStudentAndGroupAsync(int studentId, int groupId);
        public Task AcceptOrDeclineInviteAsync(GroupMember existed, bool isAccepted);
        public Task AcceptOrDeclineRequestAsync(GroupMember existed, bool isAccepted);
        public Task<JoinInvite> GetInviteOfStudentAndGroupAsync(int accountId, int groupId);
        public Task<JoinRequest> GetRequestOfStudentAndGroupAsync(int accountId, int groupId);
    }
}