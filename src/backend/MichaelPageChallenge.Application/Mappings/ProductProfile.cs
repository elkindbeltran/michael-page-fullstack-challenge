namespace MichaelPageChallenge.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}