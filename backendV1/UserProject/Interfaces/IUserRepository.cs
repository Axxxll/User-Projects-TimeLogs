public interface IUserRepository
{
    public ICollection<User> GetUsers();
    public User GetUser(int userId);

    public Task<ICollection<UserWithTotalHoursDto>> GetTopTenUsers();
    public PagedResult<UsersTimeLogsDto> GetUserTimeLogs(int pageNumber, int pageSize);
    public bool UserExists(int userId);
}