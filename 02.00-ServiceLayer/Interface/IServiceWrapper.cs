using ServiceLayer.Interface.Auth;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.Interface
{
    public interface IServiceWrapper
    {
        public IAccountService Accounts { get; }
        public IAuthService Auth { get; }
    }
}
