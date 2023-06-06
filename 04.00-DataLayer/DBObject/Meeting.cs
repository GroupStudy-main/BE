using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace DataLayer.DBObject
{
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int CountMember { get; set; }
        public DateTime? Start { get; set; } = null;
        public DateTime? End { get; set; } = null;
        public DateTime? ScheduleStart { get; set; } = null;
        public DateTime? ScheduleEnd { get; set; } = null;

        #region  Group
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        #endregion

        #region Connection
        public virtual ICollection<Connection> Connections { get; set; } = new Collection<Connection>();
        #endregion
    }
}