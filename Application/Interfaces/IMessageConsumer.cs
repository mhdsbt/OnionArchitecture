using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMessageConsumer
    {
        Task ConsumeAsync(object message);
    }
}
