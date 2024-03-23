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
	public class AutoCallRequestController : BaseApiController
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateAutoCallCommand command)
		{
			return Ok(await Mediator.Send(command));
		}
	}
}