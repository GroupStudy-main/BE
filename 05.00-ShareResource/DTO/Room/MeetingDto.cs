using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class MeetingDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string UserId { get; set; }
        //public string UserName { get; set; }
        //public string DisplayName { get; set; }
        public int CountMember { get; set; }
    }
}
