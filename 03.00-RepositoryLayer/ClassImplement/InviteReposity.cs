using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class InviteReposity : BaseRepo<JoinInvite, int>, IInviteReposity
    {
        public InviteReposity(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(JoinInvite entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<JoinInvite> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<JoinInvite> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(JoinInvite entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}