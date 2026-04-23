namespace MichaelPageChallenge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Fetching products");

        var result = await _mediator.Send(new GetProductsQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Fetching product with Id {ProductId}", id);

        var result = await _mediator.Send(new GetProductByIdQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        _logger.LogInformation("Creating product");

        await _mediator.Send(command);

        return Ok();
    }
}