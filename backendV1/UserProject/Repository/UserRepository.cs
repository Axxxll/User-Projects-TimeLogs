
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{

    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public User GetUser(int userId)
    {
        return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
    }

    public ICollection<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public ICollection<User> GetUserWithMostTime()
    {
        throw new NotImplementedException();
    }

    public User GetUserWithTimeLogs(int userId)
    {
        var userWithTimelogs = _context.TimeLogs.Where(t => t.UserId == userId).GroupBy(t => new {t.User.FirstName, t.User.LastName, t.User.Email, t.User.TimeLogs}).FirstOrDefault();

        return (User)userWithTimelogs;
    }

    public bool UserExists(int userId)
    {
        return _context.Users.Any(u => u.Id == userId);
    }
}