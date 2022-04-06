using Google.Protobuf;
using Medio.Proto;

namespace Medio.Network.MessageHandlers;

public abstract class MessageHandlerBase<T> : IMessageHandler
    where T : IMessage<T>, new()
{
    public void Handle(byte[] message)
    {
        var msg = ParseMessage(message);
        Process(msg);
    }

    private static T ParseMessage(byte[] msg)
    {
        var byteArrMsg = new ByteArr(msg);
        MessageParser<T> parser = new(() => new T());
        var message = parser.ParseFrom(byteArrMsg.Data);
        return message;
    }
    protected abstract void Process(T message);
}
