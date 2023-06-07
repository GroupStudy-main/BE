using System.ComponentModel.DataAnnotations;
using ShareResource.DTO.MeetingRoom;

namespace ShareResource.DTO.Group;

public class GroupGetDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    #region Group Member
    public virtual ICollection<GroupMemberGetDto> GroupMembers { get; set; }
    #endregion
        
    #region Meeting Room
    public virtual ICollection<MeetingRoomGetDto> MeetingRooms { get; set; }
    #endregion
}