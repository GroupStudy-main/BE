using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShareResource.DTO.Connection;
using ShareResource.DTO.Group;
using ShareResource.DTO.MeetingRoom;

namespace ShareResource.DTO.Meeting;

public class MeetingGetDto : BaseGetDto
{
    public int Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public DateTime? ScheduleStart { get; set; }
    public DateTime? ScheduleEnd { get; set; }
    public int MeetingRoomId { get; set; }
    public int GroupId { get; set; }
    public string Name { get; set; }
    public int CountMember { get; set; }
}