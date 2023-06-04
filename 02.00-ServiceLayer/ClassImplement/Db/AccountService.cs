using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.ClassImplement.Db
{
    public class AccountService : IAccountService
    {
        private IRepoWrapper repos;

        public AccountService(IRepoWrapper repos)
        {
            this.repos = repos;
        }
        public IQueryable<Account> GetList()
        {
            return repos.Accounts.GetList();
        }
        public IQueryable<Account> SearchStudents(string search)
        {
            search = search.ToLower().Trim();
            return repos.Accounts.GetList()
                .Where(e =>
                EF.Functions.Like(e.Id.ToString(), search+"%")
                //e.Id.ToString().Contains(search)
                    //SqlFunctions.StringConvert((double)e.Id) 
                    || e.Email.ToLower().Contains(search)
                    || e.Username.ToLower().Contains(search)
                    || e.FullName.ToLower().Contains(search)
                );
        }


        public async Task<Account> GetByIdAsync(int id)
        {
            return await repos.Accounts.GetByIdAsync(id);
        }

        public async Task<Account> GetProfileByIdAsync(int id)
        {
            return await repos.Accounts.GetProfileByIdAsync(id);
        }

        public async Task<Account> GetAccountByUserNameAsync(string userName)
        {
            Account account = await repos.Accounts.GetByUsernameAsync(userName);
            return account;
        }
        public async Task CreateAsync(Account entity)
        {
            await repos.Accounts.CreateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await repos.Accounts.RemoveAsync(id);
        }

        public async Task UpdateAsync(Account entity)
        {
            await repos.Accounts.UpdateAsync(entity);
        }

    }
}
