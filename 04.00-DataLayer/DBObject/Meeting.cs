using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.DBObject
{
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public DateTime? ScheduleStart { get; set; }
        public DateTime? ScheduleEnd { get; set; }

        #region  Meeting Room
        [ForeignKey("MeetingRoomId")]
        public int MeetingRoomId { get; set; }
        public virtual MeetingRoom MeetingRoom { get; set; }
        #endregion

        #region Connection
        public virtual ICollection<Connection> Connections { get; set; }
        #endregion
    }
}