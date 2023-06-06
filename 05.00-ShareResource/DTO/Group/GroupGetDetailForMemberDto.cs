using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class GroupGetDetailForMemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentGetDto> Members { get; set; }
        public virtual ICollection<LiveMeetingGetDto> LiveMeetings { get; set; }
        public virtual ICollection<ScheduleMeetingGetDto> ScheduleMeetings { get; set; }
    }
}
