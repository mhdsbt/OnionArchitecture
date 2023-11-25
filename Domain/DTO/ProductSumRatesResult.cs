using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
	public class ProductSumRatesResult
	{
		public decimal TotalRates { get; set; }
		public List<ProductDescription> Products { get; set; }

		public ProductSumRatesResult()
		{
			Products = new List<ProductDescription>();
		}
	}
}
