using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO.Account
{
    public class AccountProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }

        public string RoleName { get; set; }

        // Group Member
        public virtual ICollection<GroupGetListDto> LeadGroups { get; set; }
        public virtual ICollection<GroupGetListDto> JoinGroups { get; set; }
    }
}
