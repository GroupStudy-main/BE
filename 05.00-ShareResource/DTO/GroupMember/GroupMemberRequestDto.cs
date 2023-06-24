using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class JoinRequestGetDto
    {
        public int Id { get; set; }
        public string RequestMessage { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }

    public class GroupMemberRequestCreateDto
    {
        public string RequestMessage { get; set; }
        public int GroupId { get; set; }
        public int AccountId { get; set; }
    }
}
