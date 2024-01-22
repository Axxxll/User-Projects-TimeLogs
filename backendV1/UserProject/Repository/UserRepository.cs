
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{

    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ICollection<User>> GetTopTenUsers()
    {

        var usersWithHours = await _context.Users
        .Join(_context.TimeLogs, u => u.Id, t => t.UserId, (u, t) => new { User = u, t.HoursWorked })
        .ToListAsync();

        var topUsers = usersWithHours
            .GroupBy(ut => ut.User.Id)
            .Select(g => new { User = g.First().User, TotalHours = g.Sum(x => x.HoursWorked) })
            .OrderByDescending(g => g.TotalHours)
            .Take(10)
            .Select(g => g.User)
            .ToList();

        return topUsers;
    }

    public User GetUser(int userId)
    {
        return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
    }

    public ICollection<User> GetUsers()
    {
        return _context.Users.ToList();
    }


    public User GetUserWithTimeLogs(int userId)
    {
        var userWithTimelogs = _context.TimeLogs.Where(t => t.UserId == userId).GroupBy(t => new { t.User.FirstName, t.User.LastName, t.User.Email, t.User.TimeLogs }).FirstOrDefault();

        return (User)userWithTimelogs;
    }

    public bool UserExists(int userId)
    {
        return _context.Users.Any(u => u.Id == userId);
    }
}