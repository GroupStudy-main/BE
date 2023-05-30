using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public class GroupRepository : BaseRepo<Group, int>, IGroupRepository
    {
        public GroupRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(Group entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Group> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<Group> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Group entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
