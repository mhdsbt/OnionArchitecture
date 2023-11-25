using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetProductSumRatesQuery : IRequest<ProductSumRatesResult>
	{
		public ProductIds ProductIds { get; set; }
		public class GetProductSumRatesQueryHandler : IRequestHandler<GetProductSumRatesQuery, ProductSumRatesResult>
		{
			private readonly IApplicationDbContext _context;

			public GetProductSumRatesQueryHandler(IApplicationDbContext context)
			{
				_context = context;
			}

			public async  Task<ProductSumRatesResult> Handle(GetProductSumRatesQuery request, CancellationToken cancellationToken)
			{

				var firstProduct = _context.Products.Where(a => a.Id == request.ProductIds.firstProductId).FirstOrDefault();

				var secondProduct = _context.Products.Where(a => a.Id == request.ProductIds.secondProductId).FirstOrDefault();

				ProductSumRatesResult result = new Domain.DomainServices.ProductSumRates(firstProduct, secondProduct).GetProductSumRates();
				if (result == null) return null;

				return  result;
			}
		}
	}
}
