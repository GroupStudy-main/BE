using DataLayer.DBObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Db
{
    public interface IAccountService : IBaseDbService<Account, int>
    {
        public Task<Account> GetAccountByUserNameAsync(string userName);
    }
}
