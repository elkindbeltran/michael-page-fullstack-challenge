namespace MichaelPageChallenge.Application.Features.Products.Commands;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Unit>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        product.Id = Guid.NewGuid();

        await _repository.AddAsync(product);

        return Unit.Value;
    }
}