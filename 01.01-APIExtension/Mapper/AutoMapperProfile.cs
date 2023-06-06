using AutoMapper;
using DataLayer.DBObject;
using ShareResource.DTO;
using ShareResource.Enums;

namespace ShareResource.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<src, dest>
            MapAccount();

            MapGroup();

            MapRole();

            MapGroupMember();

            MapMeeting();
            //CreateMap<MeetingRoom, MeetingDto>();
            //.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

        }

        private void MapMeeting()
        {
            CreateMap<Meeting, ScheduleMeetingGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .PreserveReferences();
            CreateMap<Meeting, LiveMeetingGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .PreserveReferences();
            CreateMap<Meeting, PastMeetingGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .PreserveReferences();

            CreateMap<ScheduleMeetingCreateDto, Meeting>();
            CreateMap<InstantMeetingCreateDto, Meeting>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(
                    src => DateTime.Now));
        }

        private void MapGroupMember()
        {
            //Invite
            CreateMap<GroupMember, GroupMemberInviteGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(
                    src => src.Account.Username))
                .PreserveReferences();
            CreateMap<GroupMemberInviteCreateDto, GroupMember>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => GroupMemberState.Inviting));
            //Request
            CreateMap<GroupMember, GroupMemberRequestGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(
                    src => src.Account.Username))
                .PreserveReferences();
            CreateMap<GroupMemberRequestCreateDto, GroupMember>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => GroupMemberState.Requesting));
        }

        private void MapRole()
        {
            CreateMap<Role, RoleGetDto>()
                .PreserveReferences();
        }

        private void MapGroup()
        {
            CreateMap<GroupCreateDto, Group>()
                .ForMember(dest => dest.GroupSubjects, opt => opt.MapFrom(
                    src => src.SubjectIds.Select(id => new GroupSubject { SubjectId = (int)id })
                ));
            CreateMap<Group, GroupGetListDto>()
                .ForMember(dest => dest.MemberCount, opt => opt.MapFrom<int>(
                    src => src.GroupMembers
                        .Where(e => e.State == GroupMemberState.Leader|| e.State == GroupMemberState.Member)
                        .Count()))
                .PreserveReferences();

            CreateMap<Group, GroupGetDetailForLeaderDto>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(
                    src => src.GroupMembers
                        .Where(e => e.State == GroupMemberState.Leader || e.State == GroupMemberState.Member)
                        .Select(e => e.Account)))
                .ForMember(dest => dest.JoinRequest, opt => opt.MapFrom(
                    src => src.GroupMembers
                        .Where(e => e.State == GroupMemberState.Requesting)))
                //Past
                .ForMember(dest => dest.PastMeetings, opt => opt.MapFrom(
                    src => src.Meetings
                        .Where(e => (e.End != null || (e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today)))))
                 //Live
                .ForMember(dest => dest.LiveMeetings, opt => opt.MapFrom(
                    src => src.Meetings
                        .Where(e => e.Start != null && e.End == null)))
                //Schedule
                .ForMember(dest => dest.ScheduleMeetings, opt => opt.MapFrom(
                    src => src.Meetings
                        .Where(e => (e.ScheduleStart != null && e.ScheduleStart.Value.Date >= DateTime.Today && e.Start == null))))
                .PreserveReferences();

            CreateMap<Group, GroupGetDetailForMemberDto>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(
                    src => src.GroupMembers
                        .Where(e => e.State == GroupMemberState.Leader || e.State == GroupMemberState.Member)
                        .Select(e => e.Account)))

                .ForMember(dest => dest.LiveMeetings, opt => opt.MapFrom(
                    src => src.Meetings
                        .Where(e => e.Start != null && e.End == null)))
                .ForMember(dest => dest.ScheduleMeetings, opt => opt.MapFrom(
                    src => src.Meetings
                        .Where(e => e.ScheduleStart != null && e.ScheduleStart.Value.Date >= DateTime.Today && e.Start == null)))
                .PreserveReferences();
        }

        private void MapAccount()
        {
            BasicMap<Account, StudentGetDto, AccountRegisterDto, AccountUpdateDto>();
            CreateMap<Account, MemberDto>();
            CreateMap<Account, AccountProfileDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(
                    src => src.Role.Name))
                .ForMember(dest => dest.LeadGroups, opt => opt.MapFrom(
                    src => src.GroupMembers.Where(e => e.State == GroupMemberState.Leader).Select(e => e.Group)))
                .ForMember(dest => dest.JoinGroups, opt => opt.MapFrom(
                    src => src.GroupMembers.Where(e => e.State == GroupMemberState.Member).Select(e => e.Group)))
                .PreserveReferences();
        }

        private void BasicMap<TDbEntity, TGetDto, TCreateDto, TUpdateDto>()
            where TGetDto : BaseGetDto 
            where TCreateDto : BaseCreateDto 
            where TUpdateDto : BaseUpdateDto
        {
            CreateMap<TDbEntity, TGetDto>()
                .PreserveReferences()  //Tránh bị đè lặp
                .ReverseMap();
            CreateMap<TCreateDto, TDbEntity>();
            CreateMap<TUpdateDto, TDbEntity>();
        }
        private void BasicMap<TDbEntity, TGetDto, TCreateDto, TUpdateDto, TOdataDto>()
            where TGetDto : BaseGetDto
            where TCreateDto : BaseCreateDto
            where TUpdateDto : BaseUpdateDto
            where TOdataDto : BaseOdataDto
        {
            //CreateMap<TDbEntity, TGetDto>()
            //    .PreserveReferences()//Tránh bị đè
            //    .ReverseMap();
            //CreateMap<TCreateDto, TDbEntity>();
            //CreateMap<TUpdateDto, TDbEntity>();
            BasicMap<TDbEntity, TGetDto, TCreateDto, TUpdateDto>();
            CreateMap<TDbEntity, TOdataDto>()
                .ForAllMembers(o => o.ExplicitExpansion());
        }
    }
}
