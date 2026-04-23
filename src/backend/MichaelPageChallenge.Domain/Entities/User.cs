namespace MichaelPageChallenge.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    private User() { }

    public User(string name, string email)
    {
        Name = name;
        Email = email;        
    }
}