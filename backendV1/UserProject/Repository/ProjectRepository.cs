public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<ProjectUserHoursDto> GetTopTenProjects()
    {
        var projectsHours = _context.Projects
        .Join(_context.TimeLogs, p => p.Id, t => t.ProjectId, (p, t) => new {p, t})
        .Join(_context.Users, pt => pt.t.UserId, u => u.Id, (pt, u) => new {pt.p, pt.t, u})
        .GroupBy(g => new { g.t.ProjectId, g.u.Id})
        .Select(g => new ProjectUserHoursDto
        {
            ProjectId = g.First().p.Id,
            ProjectName = g.First().p.Name,
            HoursWorked = g.Sum(x => x.t.HoursWorked),
            UserName = g.First().u.FirstName + " " + g.First().u.LastName,
            UserId = g.First().u.Id
        })
        .OrderByDescending(g => g.HoursWorked)
        .Take(10)
        .ToList();

        return projectsHours;
    }
}