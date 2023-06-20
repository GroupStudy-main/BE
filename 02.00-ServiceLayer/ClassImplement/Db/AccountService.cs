using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public async Task<Account> GetByIdAsync(int id)
        {
            return await repos.Accounts.GetByIdAsync(id);
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

        public async Task<bool> ExistAsync(int id)
        {
            return await repos.Accounts.IdExistAsync(id);
        }

        public async Task<bool> ExistUsernameAsync(string username)
        {
            return await repos.Accounts.GetList().AnyAsync(x => x.Username == username);
        }

        public async Task<bool> ExistEmailAsync(string email)
        {
            return await repos.Accounts.GetList().AnyAsync(x => x.Email == email);
        }
    }
}
