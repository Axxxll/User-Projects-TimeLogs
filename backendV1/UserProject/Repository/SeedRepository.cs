public class SeedRepository : ISeedRepository
{

    private readonly DataContext _context;

    public SeedRepository(DataContext context)
    {
        _context = context;
    }

    public bool IsEmptyOrClearData()
    {

        if(!_context.TimeLogs.Any())
        {
            return true;
        }
        _context.Users.RemoveRange(_context.Users);
        _context.Projects.RemoveRange(_context.Projects);
        _context.TimeLogs.RemoveRange(_context.TimeLogs);

        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool Seed()
    {
        if (!IsEmptyOrClearData())
        {
            return false;
        }

        Random random = new Random();

        var firstNames = new List<string>()
        {"John", "Gringo", "Mark", "Lisa", "Maria", "Sonya", "Philip", "Jose", "Larenzo", "George", "Justin"};

        var lastNames = new List<string>()
        {
            "Johnson", "Lamas","Jackson", "Brown", "Mason", "Rodriguez", "Roberts", "Thomas", "Rose", "McDonalds"
        };
        var domains = new List<string>()
        {"hotmail.com", "gmail.com", "live.com"};

        // Generate Users
        var users = new List<User>();

        for (int i = 0; i < 100; i++)
        {
            string firstName = firstNames[random.Next(firstNames.Count)];
            string lastName = lastNames[random.Next(lastNames.Count)];
            string domain = domains[random.Next(domains.Count)];

            User user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = $"{firstName}.{lastName}@{domain}".ToLower()
            };

            users.Add(user);

        }

        //Generate Projects
        List<Project> projects = new List<Project>
            {
                new Project { Id = 1, Name = "My own" },
                new Project { Id = 2, Name = "Work" },
                new Project { Id = 3, Name = "Free time" }
            };

        //Generate TimeLogs
        List<TimeLog> timeLogs = new List<TimeLog>();

        DateTime startDate = DateTime.Now.AddDays(-100); 

        Dictionary<int, Dictionary<DateTime, float>> userHours = new Dictionary<int, Dictionary<DateTime, float>>();

        foreach (var user in users)
        {
            userHours[user.Id] = new Dictionary<DateTime, float>();

            int entries = random.Next(1, 20);
            for (int i = 0; i < entries; i++)
            {
                Project project = projects[random.Next(projects.Count)];

                DateTime entryDate = startDate.AddDays(random.Next(0, 101)); 

                if (!userHours[user.Id].ContainsKey(entryDate))
                {
                    userHours[user.Id][entryDate] = 0;
                }

                float hoursWorked = (float)Math.Round(random.NextDouble() * (Math.Min(8.00, 8.00 - userHours[user.Id][entryDate]) - 0.25) + 0.25, 2);

                userHours[user.Id][entryDate] += hoursWorked;

                TimeLog timeLog = new TimeLog
                {
                    User = user,
                    UserId = user.Id,
                    Project = project,
                    ProjectId = project.Id,
                    HoursWorked = hoursWorked,
                    Date = entryDate
                };

                timeLogs.Add(timeLog);
            }
        }

        _context.TimeLogs.AddRange(timeLogs);

        return Save();
    }
}