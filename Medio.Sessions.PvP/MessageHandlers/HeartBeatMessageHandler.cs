﻿using Medio.Network.ClientPools;
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
    ILogger? _logger;
    Timer _timer;
    public HeartBeatMessageHandler(ClientPool clientPool, int period, Client client)
    {
        _logger = LogManager.GetCurrentClassLogger();
        _clientPool = clientPool;
        _timer = new Timer((o) =>
        {
            var diff = (TimeOnly.FromDateTime(DateTime.Now) - _lastHeartBeatMessage);
            if (diff.Ticks >= TimeSpan.FromSeconds(period).Ticks)
            {
                _clientPool.Remove(client.Id);
                _timer?.Dispose();
            }

        }, null, 0, 2_000);

    }

    protected override void Process(HeartBeatMessage message)
    {
        if (_clientPool.Clients.ContainsKey(message.Id) == false)
            throw new InvalidRequestException(message, "wrong id");

        _lastHeartBeatMessage = TimeOnly.FromDateTime(DateTime.Now);
    }
}
