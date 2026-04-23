namespace MichaelPageChallenge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Fetching users");

        var result = await _mediator.Send(new GetUsersQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        _logger.LogInformation("Creating user");

        var result = await _mediator.Send(command);

        return Created("", result);
    }
}