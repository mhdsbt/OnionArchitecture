using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
	public class Product:BaseEntity
	{
		public string Name { get; set; }
		public string BarCode { get; set; }
		public string Description { get; set; }
		public string Rate {  get; set; }

	}
}
