using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBObject
{
    public class Connection
    {
        [Key]
        public string Id { get; set; }   
        DateTime Start { get;set; }
        DateTime? End { get;set; }

        #region Meeting
        [ForeignKey("MeetingId")]
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        #endregion


        #region Student
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public string UserName { get; set; }

        public virtual Account Account { get; set; }
        #endregion
    }
}