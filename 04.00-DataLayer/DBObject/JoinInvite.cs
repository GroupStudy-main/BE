using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBObject
{
    public class JoinInvite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Group
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        //Student
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}