using Application.Interfaces;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.MessageConsumer
{
    public class CapMessageConsumer : IMessageConsumer, ICapSubscribe

    {
        [CapSubscribe("AutoCallRequest")]
        public async Task ConsumeAsync(string eventName, object message)
        {
            Console.WriteLine($"Received message from event '{eventName}': {message.ToString()}");
            await Task.CompletedTask;
        }
    }
}
