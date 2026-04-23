using MichaelPageChallenge.Domain.Enums;

namespace MichaelPageChallenge.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public TaskItemStatus Status { get; private set; }

    private TaskItem() { }

    public TaskItem(string title, Guid userId)
    {
        SetTitle(title);
        AssignUser(userId);
        Status = TaskItemStatus.Pending;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        Title = title;
    }

    public void AssignUser(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User is required");

        UserId = userId;
    }

    public void ChangeStatus(TaskItemStatus newStatus)
    {
        if (Status == TaskItemStatus.Pending && newStatus == TaskItemStatus.Done)
            throw new InvalidOperationException("Cannot change status from Pending to Done directly");

        Status = newStatus;
    }
}