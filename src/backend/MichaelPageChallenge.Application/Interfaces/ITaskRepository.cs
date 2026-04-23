namespace MichaelPageChallenge.Application.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task);
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId);
    Task UpdateAsync(TaskItem task);
}