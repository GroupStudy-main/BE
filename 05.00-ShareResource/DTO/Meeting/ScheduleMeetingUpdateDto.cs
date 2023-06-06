using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class ScheduleMeetingUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ScheduleStart { get; set; } = null;
        public DateTime? ScheduleEnd { get; set; } = null;
    }
}
