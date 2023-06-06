using DataLayer.DBObject;
using ShareResource.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Db
{
    public interface IGroupService
    {
        public IQueryable<Group> GetList();
        public Task<Group> GetFullByIdAsync(int id);
        /// <summary>
        /// Create a group and add group leader
        /// </summary>
        /// <param name="group"></param>
        /// <param name="creatorId">id of creator account id</param>
        /// <returns></returns>
        public Task CreateAsync(Group group, int creatorId);
        //public Task UpdateAsync(Group group);
        public Task UpdateAsync(GroupUpdateDto dto);
        public Task RemoveAsync(int id);
        /// <summary>
        /// Get a list of groups student has joined
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<IQueryable<Group>> GetMemberGroupsOfStudentAsync(int studentId);

        public Task<IQueryable<Group>> GetLeaderGroupsOfStudentAsync(int leaderId);
        public Task<IQueryable<Group>> GetJoinGroupsOfStudentAsync(int studentId);
        public Task<List<int>> GetLeaderGroupsIdAsync(int leaderId);
        public Task<bool> IsStudentLeadingGroupAsync(int studentId, int groupId);
    }
}
