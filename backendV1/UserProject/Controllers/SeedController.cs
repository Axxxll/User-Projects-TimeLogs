using Microsoft.AspNetCore.Mvc;

[Route("app/[controller]")]
public class SeedController : Controller
{

    private readonly ISeedRepository _seedRepository;
    public SeedController(ISeedRepository seedRepository)
    {
        _seedRepository = seedRepository;
    }

    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult SeedData()
    {
        if(!_seedRepository.Seed())
        {
            ModelState.AddModelError("","Something went wrong!");
            return StatusCode(500, ModelState);
        }

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok("Database was reset successfully!");
    }
}