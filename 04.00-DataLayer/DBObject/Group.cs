using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.DBObject
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        #region Group Member
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        #endregion
        #region Meeting Room
        public virtual ICollection<MeetingRoom> MeetingRooms { get; set; }
        #endregion

    }
}