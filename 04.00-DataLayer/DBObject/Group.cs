using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Name { get; set; }
    }
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GroupSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //Student
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}