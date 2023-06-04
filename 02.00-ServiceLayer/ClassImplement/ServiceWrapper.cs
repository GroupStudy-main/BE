using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using ServiceLayer.Interface.Auth;
using ServiceLayer.Interface.Db;
using ServiceLayer.ClassImplement.Auth;
using Microsoft.Extensions.Configuration;
using ServiceLayer.ClassImplement.Db;

namespace ServiceLayer.ClassImplement
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IRepoWrapper repos;
        private readonly IConfiguration configuration;

        public ServiceWrapper(IRepoWrapper repos, IConfiguration configuration)
        {
            this.repos = repos;
            this.configuration = configuration;
            users = new AccountService(repos);
            auth = new AuthService(repos, configuration);
            classes = new ClassService(repos);
            subjects = new SubjectService(repos);
        }

        private IAccountService users;
        public IAccountService Accounts
        {
            get
            {
                if (users is null)
                {
                    users = new AccountService(repos);
                }
                return users;
            }
        }

        private IAuthService auth;
        public IAuthService Auth
        {
            get
            {
                if (auth is null)
                {
                    auth = new AuthService(repos, configuration);
                }
                return auth;
            }
        }

        private IGroupService groups;
        public IGroupService Groups
        {
            get
            {
                if (groups is null)
                {
                    groups = new GroupService(repos);
                }
                return groups;
            }
        }

        private IClassService classes;
        public IClassService Classes
        {
            get
            {
                if (classes is null)
                {
                    classes = new ClassService(repos);
                }
                return classes;
            }
        }

        private ISubjectService subjects;
        public ISubjectService Subjects
        {
            get
            {
                if (subjects is null)
                {
                    subjects = new SubjectService(repos);
                }
                return subjects;
            }
        }
    }
}
