using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class GroupGetListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MemberCount { get; set; }
    }
}
