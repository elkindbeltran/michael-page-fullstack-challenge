namespace MichaelPageChallenge.Application.Features.Products.Commands;

public class CreateOrderCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}