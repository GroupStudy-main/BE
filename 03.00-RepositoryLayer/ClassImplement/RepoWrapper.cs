using DataLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly GroupStudyContext dbContext;

        public RepoWrapper(GroupStudyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private IAccountRepo users;
        public IAccountRepo Accounts { 
            get 
            { 
                if(users is null)
                {
                    users = new AccountRepo(dbContext);
                }
                return users; 
            } 
        }
    }
}
