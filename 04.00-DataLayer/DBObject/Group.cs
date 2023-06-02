using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace DataLayer.DBObject
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        #region Group Member
        public virtual ICollection<GroupMember> GroupMembers { get; set; } = new Collection<GroupMember>();
        #endregion
        
        #region Meeting Room
        public virtual ICollection<MeetingRoom> MeetingRooms { get; set; }
        #endregion

        #region Class
        //Class
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public Class Class { get; set; }
        #endregion

        #region Subjects
        public virtual ICollection<GroupSubject> GroupSubjects { get; set; }
        #endregion
    }
}