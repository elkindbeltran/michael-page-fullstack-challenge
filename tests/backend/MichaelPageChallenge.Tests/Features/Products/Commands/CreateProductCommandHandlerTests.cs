using MichaelPageChallenge.Application.Features.Products.Commands;
using MichaelPageChallenge.Application.Interfaces;
using Moq;
using AutoMapper;

namespace MichaelPageChallenge.UnitTests.Features.Products.Commands;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public CreateProductCommandHandlerTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ShouldCreateProduct()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test",
            Price = 100
        };

        var product = new Domain.Entities.Product
        {
            Name = command.Name,
            Price = command.Price
        };

        _mapperMock
            .Setup(x => x.Map<Domain.Entities.Product>(command))
            .Returns(product);

        var handler = new CreateProductCommandHandler(
            _repositoryMock.Object,
            _mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        _repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Domain.Entities.Product>()),
            Times.Once);
    }
}