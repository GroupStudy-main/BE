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
        
        private IMeetingService meetingService;
        public IMeetingService Meeting
        {
            get
            {
                if (meetingService is null)
                {
                    meetingService = new MeetingService(repos);
                }
                return meetingService;
            }
        }

        private IDocumentFileService _documentFileService;

        public IDocumentFileService DocumentFiles
        {
            get
            {
                if (_documentFileService is null)
                {
                    _documentFileService = new DocumentFileService(repos);
                }
                return _documentFileService;
            }
        }
    }
}
