using System.ComponentModel.DataAnnotations.Schema;
using ShareResource.DTO.Group;

namespace ShareResource.DTO.Meeting;

public class MeetingCreateDto : BaseCreateDto
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public DateTime? ScheduleStart { get; set; }
    public DateTime? ScheduleEnd { get; set; }
    public int MeetingRoomId { get; set; }
    public int GroupId { get; set; }
    public string Name { get; set; }
    public int CountMember { get; set; }

}