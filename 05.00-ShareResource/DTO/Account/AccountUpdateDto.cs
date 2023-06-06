using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class AccountUpdateDto  : BaseUpdateDto
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }

    }
    public class AccountChangePasswordDto : BaseUpdateDto
    {
        [Key]
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
