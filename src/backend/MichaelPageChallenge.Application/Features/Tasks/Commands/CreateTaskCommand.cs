namespace MichaelPageChallenge.Application.Features.Tasks.Commands;

public record CreateTaskCommand(string Title, Guid UserId) : IRequest<TaskDto>;