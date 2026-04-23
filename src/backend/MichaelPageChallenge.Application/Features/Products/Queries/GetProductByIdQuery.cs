namespace MichaelPageChallenge.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;