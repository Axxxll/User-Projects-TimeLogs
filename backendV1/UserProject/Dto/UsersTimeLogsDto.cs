public class UsersTimeLogsDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DateTime Date { get; set; }
    public float HoursWorked { get; set; }
}