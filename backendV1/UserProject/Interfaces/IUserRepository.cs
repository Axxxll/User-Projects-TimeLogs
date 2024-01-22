public interface IUserRepository
{
    public ICollection<User> GetUsers();
    public User GetUser(int userId);

    public Task<ICollection<User>> GetTopTenUsers();
    public bool UserExists(int userId);
}