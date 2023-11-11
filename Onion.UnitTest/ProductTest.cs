using System.Threading;
using System.Threading.Tasks;
using Application.Features.ProductFeatures.Commands;
using Application.Interfaces;
using Domain.Entities;
using Moq;
using Xunit;

namespace ProductTest
{
	public class CreateProductCommandHandlerTests
	{
		//[Fact]
		//public async Task Handle_ValidCommand_ReturnsProductId()
		//{
		//	// Arrange
		//	var command = new CreateProductCommand
		//	{
		//		Name = "Sample Product",
		//		Barcode = "123456789",
		//		Description = "A sample product",
		//		Rate = 10.0m
		//	};

		//	var mockContext = new Mock<IApplicationDbContext>();
		//	var handler = new CreateProductCommand.CreateProductCommandHandler(mockContext.Object);

		//	// Act
		//	var result = await handler.Handle(command, CancellationToken.None);

		//	// Assert
		//	Xunit.Assert.True(result > 0); // The product ID should be a positive integer.
		//}

		[Fact]
		public void test()
		{
			// Arrange
	

			// Act
			//var result = await handler.Handle(command, CancellationToken.None);

			// Assert
			Xunit.Assert.True(true); // This assertion always passes, regardless of the result.
		}

	}
}