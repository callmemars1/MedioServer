using Medio.Network.Clients;

namespace Medio.Network.MessageHandlers;

public interface IMessageHandlerCreator
{
    int MessageTypeID { get; }
    IMessageHandler Create(Client client);
}
