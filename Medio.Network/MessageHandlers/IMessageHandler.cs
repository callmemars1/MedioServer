namespace Medio.Network.MessageHandlers;

public interface IMessageHandler
{
    void Handle(byte[] message);
}
