using AutoMapper;
using DataLayer.DBObject;
using ShareResource.DTO;
using ShareResource.DTO.Group;
using ShareResource.DTO.Meeting;
using ShareResource.DTO.MeetingRoom;

namespace ShareResource.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            BasicMap<Account, AccountGetDto, AccountRegisterDto, AccountUpdateDto>();
            CreateMap<Account, MemberDto>();


            CreateMap<MeetingRoom, RoomDto>();
            
            BasicMap<Meeting, MeetingGetDto, MeetingCreateDto, MeetingUpdateDto>();
            CreateMap<Meeting, MeetingCreateDto>();
            CreateMap<Meeting, MeetingGetDto>();

            CreateMap<Group, GroupGetDto>();
            //.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

        }
        void BasicMap<TDbEntity, TGetDto, TCreateDto, TUpdateDto>()
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
