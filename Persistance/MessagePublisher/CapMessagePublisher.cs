// In Infrastructure Layer
using Application.Interfaces;
using DotNetCore.CAP;

public class CapMessagePublisher : IMessagePublisher
{
    private readonly ICapPublisher _capPublisher;

    public CapMessagePublisher(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public async Task PublishAsync(string eventName, object message)
    {
        await _capPublisher.PublishAsync(eventName, message);
    }
}
