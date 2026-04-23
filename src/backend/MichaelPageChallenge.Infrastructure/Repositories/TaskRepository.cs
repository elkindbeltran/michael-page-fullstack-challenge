namespace MichaelPageChallenge.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Tasks
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
}