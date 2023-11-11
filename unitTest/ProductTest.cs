using Application.Features.ProductFeatures.Commands;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace unitTest
{
	public class ProductTest
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task CreateProductTest()
		{
			// Arrange
			var command = new CreateProductCommand
			{
				Name = "Sample Product",
				Barcode = "123456789",
				Description = "A sample product",
				Rate = 10.0m
			};

			
			var mockContext = new Mock<IApplicationDbContext>();
			var mockProducts = new Mock<DbSet<Product>>();

			// Set up the behavior of _context.Products
			mockContext.Setup(c => c.Products).Returns(mockProducts.Object);

			var product = new Product
			{
				Id = 1, 
			};
			mockContext.Setup(x => x.Products.Add(It.IsAny<Product>()))
								 .Callback((Product p) => p.Id = 1); // Set the ID when a product is added

			mockContext.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);


			var handler = new CreateProductCommand.CreateProductCommandHandler(mockContext.Object);

			// Act
			var result = await handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.True(result > 0);
			

		}
	}
}
