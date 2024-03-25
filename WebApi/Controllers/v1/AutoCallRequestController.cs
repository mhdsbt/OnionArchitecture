using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Domain.DTO;
using Domain.Entities;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
	[ApiVersion("1.0")]
	public class AutoCallRequestController : BaseApiController
	{
		private readonly ICapPublisher _capPublisher;

        public AutoCallRequestController(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [HttpPost]
		public async Task<IActionResult> Create(CreateAutoCallCommand command)
		{
			await Mediator.Send(command);


			var payLoad = new
			{
				PhoneNumber = command.PhoneNumber,
				AutoCallRequestStatus = command.AutoCallRequestStatus
			};

			await _capPublisher.PublishAsync("AutoCallRequest", payLoad);

            return Ok();
		}
	}
}