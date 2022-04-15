using Medio.Network.Clients;
using Medio.Network.MessageHandlers;
using Medio.Proto.MessageHandlers;

namespace Medio.Proto.MessageHandlers;

public class MessageHandlerCreatorManager
{
    readonly HashSet<IMessageHandlerCreator> _creators = new();
    public void RegisterCreator(IMessageHandlerCreator creator) 
    {
        _creators.Add(creator);
    }

    public MessageHandlerManager GetHandlerManager(Client client) 
    {
        var handlerManager = new MessageHandlerManager();
        foreach (var creator in _creators)
            handlerManager.RegisterHandler(creator.MessageTypeID, creator.Create(client));

        return handlerManager;
    }
}
