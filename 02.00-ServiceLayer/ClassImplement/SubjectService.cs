using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;

namespace ServiceLayer.ClassImplement
{
    internal class SubjectService : ISubjectService
    {
        private IRepoWrapper repos;

        public SubjectService(IRepoWrapper repos)
        {
            this.repos = repos;
        }

        public IQueryable<Subject> GetList()
        {
            return repos.Subjects.GetList();
        }
    }
}