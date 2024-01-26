using Microsoft.AspNetCore.Mvc;

[Route("app/[controller]")]
[ApiController]
public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet("TopTenProjects")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectUserHoursDto>))]
    public IActionResult GetTopTenProjects() 
    {
        var projectUsers = _projectRepository.GetTopTenProjects();

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(projectUsers);
    }
}