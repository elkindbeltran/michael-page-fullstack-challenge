namespace MichaelPageChallenge.Application.DTOs.Products;

public class CreateOrderDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}