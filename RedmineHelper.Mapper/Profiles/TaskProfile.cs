namespace RedmineHelper.Mapper.Profiles
{
    using Models.Dto;
    using AutoMapper;

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<RedmineTaskDto, ExtendedRedmineTaskDto>()
                .ForMember(x => x.Hours, opt => opt.Ignore());
            CreateMap<ExtendedRedmineTaskDto, SpentTimeDto>()
                .ForMember(x => x.Hours, opt => opt.MapFrom(src => src.Hours))
                .ForMember(x => x.IssueId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
