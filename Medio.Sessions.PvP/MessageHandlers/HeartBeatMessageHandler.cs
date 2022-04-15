using Medio.Network.ClientPools;
using Medio.Network.Clients;
using Medio.Proto.Exceptions;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Messages;
using NLog;

namespace Medio.Sessions.PvP.MessageHandlers;

public class HeartBeatMessageHandler : MessageHandlerBase<HeartBeatMessage>
{
    private readonly ClientPool _clientPool;
    TimeOnly _lastHeartBeatMessage = TimeOnly.FromDateTime(DateTime.Now);
    Timer? _timer;
    ILogger? _logger;
    public HeartBeatMessageHandler(ClientPool clientPool, int period, Client client)
    {
        _clientPool = clientPool;
        _timer = new((o) =>
        {
            var diff = (TimeOnly.FromDateTime(DateTime.Now) - _lastHeartBeatMessage);
            if (diff.Ticks >= TimeSpan.FromSeconds(period).Ticks)
                _clientPool.Remove(client.Id);

        }, null, 0, 5_000);
    }

    protected override void Process(HeartBeatMessage message)
    {
        if (_clientPool.Clients.ContainsKey(message.Id) == false)
            throw new InvalidRequestException(message, "wrong id");

        _lastHeartBeatMessage = TimeOnly.FromDateTime(DateTime.Now);
        _logger?.Info("HeartBeat");
    }
}
