using Microsoft.AspNetCore.Mvc;

[Route("app/[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetUsers();
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(users);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public IActionResult GetUserById(int userId)
    {
        if(!_userRepository.UserExists(userId))
        {
            return NotFound();
        }

        var user = _userRepository.GetUser(userId);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(user);
    }


}