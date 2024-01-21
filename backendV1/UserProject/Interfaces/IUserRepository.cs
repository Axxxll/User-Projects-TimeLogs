public interface IUserRepository
{
    public ICollection<User> GetUsers();
    public User GetUser(int userId);

    public User GetUserWithTimeLogs(int userId);
    public bool UserExists(int userId);
    public ICollection<User> GetUserWithMostTime();
}