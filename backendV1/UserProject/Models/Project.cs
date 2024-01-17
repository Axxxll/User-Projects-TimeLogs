public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<TimeLog> TimeLogs { get; set; }
}