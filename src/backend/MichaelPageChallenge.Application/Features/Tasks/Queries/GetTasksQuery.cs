namespace MichaelPageChallenge.Application.Features.Tasks.Queries;

public record GetTasksQuery() : IRequest<IEnumerable<TaskDto>>;