using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IRepoWrapper
    {
        public IAccountRepo Accounts { get; }
        public IMeetingRoomRepository Rooms { get; }
        public IMeetingRepository Meetings { get; }
        public IGroupRepository Groups { get; }
        
    }
}
