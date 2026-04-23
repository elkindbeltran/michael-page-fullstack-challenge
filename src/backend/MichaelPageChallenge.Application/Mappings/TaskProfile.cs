namespace MichaelPageChallenge.Application.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskItem, TaskDto>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<CreateTaskCommand, TaskItem>()
            .ConstructUsing(src => new TaskItem(src.Title, src.UserId, src.AdditionalData))
            .AfterMap((src, dest) =>
            {
                if (!string.IsNullOrWhiteSpace(src.AdditionalData))
                {
                    dest.SetAdditionalData(src.AdditionalData);
                }
            });
    }
}