using DataLayer.DBObject;

namespace RepositoryLayer.Interface
{
    public interface IAccountRepo : IBaseRepo<Account, int>
    {
        Task<Account> GetByUsername(string email);
        Task<Account> GetByUsernameOrEmailAndPassword(string usernameOrEmail, string password);
    }
}