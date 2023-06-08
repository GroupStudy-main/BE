namespace ShareResource.DTO
{
    public class GroupGetDetailForLeaderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentGetDto> Members { get; set; }
        public ICollection<GroupMemberRequestGetDto> JoinRequest { get; set; }
        public ICollection<GroupMemberInviteGetDto> JoinInvite { get; set; }
        public ICollection<GroupMemberGetDto> DeclineRequest { get; set; }
        public virtual ICollection<PastMeetingGetDto> PastMeetings { get; set; }
        public virtual ICollection<LiveMeetingGetDto> LiveMeetings { get; set; }
        public virtual ICollection<ScheduleMeetingGetDto> ScheduleMeetings { get; set; }

    }
}
