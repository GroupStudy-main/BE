using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.DBObject
{
    public class JoinRequest
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