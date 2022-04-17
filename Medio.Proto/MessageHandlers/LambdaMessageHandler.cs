using Google.Protobuf;

namespace Medio.Proto.MessageHandlers;

public class LambdaMessageHandler<T> : MessageHandlerBase<T>
    where T : IMessage<T> , new()
{
    private readonly LambdaMessageHandler<T>.LambdaHandler _lambdaHandler;

    public delegate void LambdaHandler(T message);
    public LambdaMessageHandler(LambdaHandler lambdaHandler)
    {
        _lambdaHandler = lambdaHandler;
    }

    protected override void Process(T message)
    {
        _lambdaHandler.Invoke(message);
    }
}
