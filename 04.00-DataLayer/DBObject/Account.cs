using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBObject
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        
        //Role
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        // Group Member
        public virtual ICollection<GroupMember> GroupMembers { get; set; }

    }
   
}
