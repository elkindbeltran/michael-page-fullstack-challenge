using AutoMapper;
using FluentAssertions;
using MichaelPageChallenge.Application.DTOs.Products;
using MichaelPageChallenge.Application.Exceptions;
using MichaelPageChallenge.Application.Features.Products.Queries;
using MichaelPageChallenge.Application.Interfaces;
using Moq;

namespace MichaelPageChallenge.UnitTests.Features.Products.Queries;

public class GetProductsQueryHandlerTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public GetProductsQueryHandlerTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedProducts()
    {
        // Arrange
        var products = new List<Domain.Entities.Product>
        {
            new() { Id = Guid.NewGuid(), Name = "Test", Price = 10 }
        };

        var productDtos = new List<ProductDto>
        {
            new() { Id = products[0].Id, Name = "Test", Price = 10 }
        };

        _repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(products);

        _mapperMock
            .Setup(x => x.Map<IEnumerable<ProductDto>>(products))
            .Returns(productDtos);

        var handler = new GetProductsQueryHandler(
            _repositoryMock.Object,
            _mapperMock.Object);

        // Act
        var result = await handler.Handle(new GetProductsQuery(), default);

        // Assert
        result.Should().BeEquivalentTo(productDtos);
        _repositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var product = new Domain.Entities.Product
        {
            Id = productId,
            Name = "Test",
            Price = 10
        };

        var productDto = new ProductDto
        {
            Id = productId,
            Name = "Test",
            Price = 10
        };

        _repositoryMock
            .Setup(x => x.GetByIdAsync(productId))
            .ReturnsAsync(product);

        _mapperMock
            .Setup(x => x.Map<ProductDto>(product))
            .Returns(productDto);

        var handler = new GetProductByIdQueryHandler(
            _repositoryMock.Object,
            _mapperMock.Object);

        // Act
        var result = await handler.Handle(
            new GetProductByIdQuery(productId),
            default);

        // Assert
        result.Should().BeEquivalentTo(productDto);

        _repositoryMock.Verify(x => x.GetByIdAsync(productId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _repositoryMock
            .Setup(x => x.GetByIdAsync(productId))
            .ReturnsAsync((Domain.Entities.Product?)null);

        var handler = new GetProductByIdQueryHandler(
            _repositoryMock.Object,
            _mapperMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(
            new GetProductByIdQuery(productId),
            default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Product with id '{productId}' was not found.");

        _repositoryMock.Verify(x => x.GetByIdAsync(productId), Times.Once);
    }
}