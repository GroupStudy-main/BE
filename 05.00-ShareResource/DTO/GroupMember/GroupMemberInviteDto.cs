using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class JoinInviteGetDto
    {
        public int Id { get; set; }
        //public string InviteMessage { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }

    public class GroupMemberInviteCreateDto
    {
        //public string InviteMessage { get; set; }
        public int GroupId { get; set; }
        public int AccountId { get; set; }
    }
}
