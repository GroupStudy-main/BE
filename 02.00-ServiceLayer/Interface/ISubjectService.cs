using DataLayer.DBObject;

namespace ServiceLayer.Interface
{
    public interface ISubjectService
    {
        IQueryable<Subject> GetList();
    }
}