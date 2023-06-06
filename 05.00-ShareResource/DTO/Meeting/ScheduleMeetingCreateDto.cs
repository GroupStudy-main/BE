using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class ScheduleMeetingCreateDto  : BaseCreateDto
    {
        public string Name { get; set; }
        public DateTime ScheduleStart { get; set; } 
        public DateTime ScheduleEnd { get; set; }
        public int GroupId { get; set; }

    }
}
