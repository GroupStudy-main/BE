using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class RequestReposity : BaseRepo<JoinRequest, int>, IRequestReposity
    {
        public RequestReposity(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(JoinRequest entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<JoinRequest> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<JoinRequest> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(JoinRequest entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}