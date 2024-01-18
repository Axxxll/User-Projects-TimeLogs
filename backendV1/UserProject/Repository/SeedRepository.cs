public class SeedRepository : ISeedRepository
{

    private readonly DataContext _context;

    public SeedRepository(DataContext context)
    {
        _context = context;
    }

    public bool ClearData()
    {
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
        if (!ClearData())
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

            // _context.Users.AddRange(user);
        }

        _context.Users.AddRange(users);

        // var projects = new List<string>()
        // {
        //     "My own", "Free Time", "Work"
        // };

        // for (int i = 0; i < 3; i++)
        // {

        // }
        return Save();
    }
}