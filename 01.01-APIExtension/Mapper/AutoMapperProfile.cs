using AutoMapper;
using DataLayer.DBObject;
using ShareResource.DTO;
using ShareResource.DTO.Account;
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
            //CreateMap<MeetingRoom, MeetingDto>();
            //.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

        }
                                            
        private void MapGroupMember()
        {
            //Invite
            CreateMap<GroupMember, GroupMemberInviteGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(
                    src => src.Account.Username));
            CreateMap<GroupMemberInviteCreateDto, GroupMember>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => GroupMemberState.Inviting));
            //Request
            CreateMap<GroupMember, GroupMemberRequestGetDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(
                    src => src.Group.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(
                    src => src.Account.Username));
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
                .PreserveReferences()
                ;
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
                    src => src.GroupMembers.Where(e => e.State == GroupMemberState.Member).Select(e => e.Group)));
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
