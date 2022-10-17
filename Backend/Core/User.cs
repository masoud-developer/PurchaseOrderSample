namespace Core;

public class User
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}