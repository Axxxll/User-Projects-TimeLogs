
using Microsoft.AspNetCore.Http.Features;
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

    public PagedResult<UsersTimeLogsDto> GetUserTimeLogs(int pageNumber, int pageSize)
    {

        var allUserTimelogs = _context.Users
        .Join(_context.TimeLogs, u => u.Id, t => t.UserId, (u, t) => new {u, t})
        .Join(_context.Projects, ut => ut.t.ProjectId, p => p.Id, (ut, p) => new {ut.u, ut.t, p})
        .GroupBy(utp => new {utp.u.Id, utp.t.ProjectId, utp.t.Date})
        .OrderByDescending(g => g.Key.Date)
        .Select(g => new UsersTimeLogsDto
            {
                UserId = g.Key.Id,
                FullName = g.First().u.FirstName + " " + g.First().u.LastName,
                Email = g.First().u.Email,
                ProjectId = g.Key.ProjectId,
                ProjectName = g.First().p.Name,
                Date = g.Key.Date,
                HoursWorked = g.Sum(x => x.t.HoursWorked)
            }
        )
        .ToList();

        var count = allUserTimelogs.Count();
        var userTimeLogs = allUserTimelogs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<UsersTimeLogsDto>
        {
            Items = userTimeLogs,
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
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