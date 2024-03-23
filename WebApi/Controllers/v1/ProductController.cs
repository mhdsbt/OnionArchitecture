using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[ApiVersion("1.0")]
	public class ProductController : BaseApiController
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateAutoCallCommand command)
		{
			return Ok(await Mediator.Send(command));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
		}

		[HttpGet("GetProductSumRates")]
		public async Task<IActionResult> GetProductSumRates([FromQuery] ProductIds productIds)
		{
			try
			{
				var result = await Mediator.Send(new GetProductSumRatesQuery() {ProductIds = productIds });

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}