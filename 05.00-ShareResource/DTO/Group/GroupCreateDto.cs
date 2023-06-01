using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class GroupCreateDto : BaseCreateDto
    {
        public string Name { get; set; }
        public int ClassId { get; set; }
        public virtual ICollection<SubjectEnum> SubjectIds { get; set; }

    }
}
