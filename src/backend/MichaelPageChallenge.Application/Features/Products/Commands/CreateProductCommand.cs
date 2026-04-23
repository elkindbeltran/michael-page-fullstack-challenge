namespace MichaelPageChallenge.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}