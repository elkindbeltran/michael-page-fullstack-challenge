namespace MichaelPageChallenge.Application.Features.Products.Queries;

public class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request,CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        return _mapper.Map<ProductDto>(product);
    }
}