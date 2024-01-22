using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<User>()
    //     .HasMany(u => u.TimeLogs)
    //     .WithOne(t => t.User)
    //     .HasForeignKey(t => t.UserId);

    //     modelBuilder.Entity<Project>()
    //         .HasMany(p => p.TimeLogs)
    //         .WithOne(t => t.Project)
    //         .HasForeignKey(t => t.ProjectId);
    // }
}