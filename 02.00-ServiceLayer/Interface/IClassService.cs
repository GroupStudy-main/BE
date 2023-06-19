using DataLayer.DBObject;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Interface
{
    public interface IClassService
    {
        public IQueryable<Class> GetList();
    }
}