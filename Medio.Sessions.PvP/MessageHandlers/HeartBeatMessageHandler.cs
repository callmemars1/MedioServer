using Medio.Network.ClientPools;
using Medio.Proto.Exceptions;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Messages;

namespace Medio.Sessions.PvP.MessageHandlers;

public class HeartBeatMessageHandler : MessageHandlerBase<HeartBeatMessage>
{
    private readonly ClientPool _clientPool;
    TimeOnly _lastHeartBeatMessage = TimeOnly.FromDateTime(DateTime.Now);
    readonly int _period;
    Timer? _timer;
    public HeartBeatMessageHandler(ClientPool clientPool, int period)
    {
        _clientPool = clientPool;
        _period = period;
    }

    protected override void Process(HeartBeatMessage message)
    {
        if (_clientPool.Clients.ContainsKey(message.Id) == false)
            throw new InvalidRequestException(message, "wrong id");

        _lastHeartBeatMessage = TimeOnly.FromDateTime(DateTime.Now);

        if (_timer == null)
        {
            _timer = new((o) =>
            {
                var diff = (TimeOnly.FromDateTime(DateTime.Now) - _lastHeartBeatMessage);
                if (diff.Ticks >= TimeSpan.FromSeconds(_period).Ticks)
                    _clientPool.Remove(message.Id);


            }, null, 0, 5_000);
        }
    }
}
