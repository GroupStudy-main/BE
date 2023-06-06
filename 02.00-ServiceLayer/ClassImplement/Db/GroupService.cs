using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.DTO;
using ShareResource.Enums;
using ShareResource.UpdateApiExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ClassImplement.Db
{
    public class GroupService  : IGroupService
    {
        private IRepoWrapper repos;

        public GroupService(IRepoWrapper repos)
        {
            this.repos = repos;
        }
        public IQueryable<Group> GetList()
        {
            return repos.Groups.GetList();
        }

        public async Task<IQueryable<Group>> GetJoinGroupsOfStudentAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers)
                .Where(e => e.AccountId == studentId && (e.State == GroupMemberState.Member || e.State == GroupMemberState.Leader))
                .Select(e => e.Group);
        }

        public async Task<IQueryable<Group>> GetMemberGroupsOfStudentAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.State == GroupMemberState.Member)
                .Select(e => e.Group);
        }

        public async Task<IQueryable<Group>> GetLeaderGroupsOfStudentAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e=>e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.State == GroupMemberState.Leader)
                .Select(e => e.Group);
        }

        public async Task<Group> GetFullByIdAsync(int id)
        {
            return await repos.Groups.GetByIdAsync(id);
        }


        public async Task CreateAsync(Group entity, int creatorId)
        {
            entity.GroupMembers.Add(new GroupMember { 
                AccountId = creatorId,
                State=GroupMemberState.Leader
            });
            await repos.Groups.CreateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await repos.Groups.RemoveAsync(id);
        }

        //public async Task UpdateAsync(Group entity)
        //{
        //    await repos.Groups.UpdateAsync(entity);
        //}

        public async Task UpdateAsync(GroupUpdateDto dto)
        {
            var group = await repos.Groups.GetByIdAsync(dto.Id);
            //Add new subject, nếu group ko có thì add
            foreach (int subjectId in dto.SubjectIds)
            {
                 if(!group.GroupSubjects.Any(e=>e.SubjectId == subjectId))
                {
                    group.GroupSubjects.Add(new GroupSubject { GroupId = group.Id, SubjectId = subjectId });
                }
            }
            //Remove subject, nếu dto ko có thì sẽ loại
            foreach (GroupSubject groupSubject in group.GroupSubjects)
            {
                if (!dto.SubjectIds.Cast<int>().Contains(groupSubject.SubjectId))
                {
                    group.GroupSubjects.Remove(groupSubject);
                }
            }
            //if (dto.())
            //{
            //    repos.G
            //}
                //group.GroupSubjects = dto.SubjectIds.Select(subId => new GroupSubject { GroupId = dto.Id, SubjectId = (int)subId }).ToList();
            group.PatchUpdate<Group, GroupUpdateDto>(dto);
            await repos.Groups.UpdateAsync(group);
        }

        public async Task<List<int>> GetLeaderGroupsIdAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.State == GroupMemberState.Leader)
                .Select(e => e.GroupId).ToList();
        }

        public async Task<bool> IsStudentLeadingGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
                .AnyAsync(e=>e.AccountId==studentId && e.GroupId==groupId && e.State == GroupMemberState.Leader);
        }
    }
}
