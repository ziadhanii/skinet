
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request.");
    }

    [HttpGet("notfound")]
    public IActionResult GetNotFound()
    {
        return NotFound("This resource was not found.");
    }

    [HttpGet("forbidden")]
    public IActionResult GetForbidden()
    {
        return Forbid();
    }


    [HttpGet("internalerror")]
    public IActionResult GetInternalError()
    {
        throw new Exception("This is a test exception");
    }
    [HttpPost("validationerror")]
    public IActionResult Getvalidationerror(CreateProductDto product)
    {
        return Ok();
    }
}
