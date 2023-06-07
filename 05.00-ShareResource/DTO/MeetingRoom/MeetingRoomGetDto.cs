using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShareResource.DTO.Group;
using ShareResource.DTO.Meeting;

namespace ShareResource.DTO.MeetingRoom;

public class MeetingRoomGetDto : BaseGetDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountMember { get; set; }
    #region Group
    [ForeignKey("GroupId")]
    public int GroupId { get; set; }
    public virtual GroupGetDto Group { get; set; }
    #endregion
    #region Meeting
    public virtual ICollection<MeetingGetDto> Meetings { get; set; }
    #endregion
}