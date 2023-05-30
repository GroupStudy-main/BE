using DataLayer.DBObject;

namespace RepositoryLayer.Interface
{
    public interface IAccountRepo : IBaseRepo<Account, int>
    {
        Task<Account> GetByUsernameAsync(string email);
        Task<Account> GetByUsernameOrEmailAndPasswordAsync(string usernameOrEmail, string password);
    }
}