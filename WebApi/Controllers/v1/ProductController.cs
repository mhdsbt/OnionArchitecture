using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.ProductFeatures.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[ApiVersion("1.0")]
	public class ProductController : BaseApiController
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateProductCommand command)
		{
			return Ok(await Mediator.Send(command));
		}
	}
}