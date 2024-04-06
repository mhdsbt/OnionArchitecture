using Application.Interfaces;
using Domain.Entities;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.MessageConsumer
{
    public class CapMessageConsumer : IMessageConsumer,ICapSubscribe

    {
        [CapSubscribe("AutoCallRequest")]
        public async Task ConsumeAsync(object message)
        {
            Console.WriteLine("Start Consuming...");
            var jsonPayload = JsonSerializer.Serialize(message);
            var autoCallRequest = JsonSerializer.Deserialize<Domain.Entities.AutoCallRequest>(jsonPayload);

            // Now you can access autoCallRequest.PhoneNumber and autoCallRequest.AutoCallRequestStatus

            // Implement your logic to process the message here
           Console.WriteLine($"Received message from event AutoCallRequest: {jsonPayload}");

            await Task.CompletedTask;
        }
    }
}
