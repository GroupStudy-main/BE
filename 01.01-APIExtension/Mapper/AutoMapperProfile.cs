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

            CreateMap<MeetingRoom, RoomDto>();
            //.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

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
            BasicMap<Account, AccountGetDto, AccountRegisterDto, AccountUpdateDto>();
            CreateMap<Account, MemberDto>();
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
