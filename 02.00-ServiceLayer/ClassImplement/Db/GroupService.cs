using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.Enums;
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


        public async Task<Group> GetByIdAsync(int id)
        {
            return await repos.Groups.GetByIdAsync(id);
        }


        public Task<Group> GetGroupByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
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

        public async Task UpdateAsync(Group entity)
        {
            await repos.Groups.UpdateAsync(entity);
        }
    }
}
