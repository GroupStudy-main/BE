using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;

namespace ServiceLayer.ClassImplement
{
    public class ClassService : IClassService
    {
        private IRepoWrapper repos;

        public ClassService(IRepoWrapper repos)
        {
            this.repos = repos;
        }

        public IQueryable<Class> GetList()
        {
            return repos.Classes.GetList();
        }
    }
}