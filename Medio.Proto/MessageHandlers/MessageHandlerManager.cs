using Medio.Network.MessageHandlers;
using Medio.Proto;

namespace Medio.Session.Client.MessageHandlers;

public class MessageHandlerManager
{
    private readonly Dictionary<int, IMessageHandler> _handlers = new();
    public void Handle(byte[] message)
    {
        var byteArr = new ByteArr(message);
        _handlers[byteArr.MessageTypeId].Handle(message);
    }
    public void RegisterHandler(int messageTypeId, IMessageHandler handler) 
    {
        if (_handlers.ContainsKey(messageTypeId))
            throw new ArgumentException("handler with key already exist");

        _handlers[messageTypeId] = handler;
    }
}
