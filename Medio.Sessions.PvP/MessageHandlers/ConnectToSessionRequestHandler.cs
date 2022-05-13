using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Network;
using Medio.Network.ClientPools;
using Medio.Proto;
using Medio.Proto.MessageHandlers;
using Medio.Proto.Messages;
using NLog;

namespace Medio.Sessions.PvP.MessageHandlers;

public class ConnectToSessionRequestHandler : MessageHandlerBase<ConnectToSessionRequest>
{
    private readonly ClientPool _clientPool;
    private readonly Map _map;
    private ILogger? _logger;

    public ConnectToSessionRequestHandler(ClientPool clientPool, Map map)
    {
        _clientPool = clientPool;
        _map = map;
        _logger = LogManager.GetCurrentClassLogger();
    }
    protected override void Process(ConnectToSessionRequest message)
    {
        try
        {
            _logger?.Info($"Processing.... id: {message.Id}");
            message.PlayerData.Map(out var player, _map.Rules.SizeIncreaseCoefficient);
            foreach (var client in _map.Entities.Values.Where((e) => e is Player))
            {
                var msg = message.PlayerData;
                if (_clientPool.Clients.TryGetValue(client.Id, out var findedClient))
                    findedClient.Send(new ByteArr(msg).ToByteArray());
            }
            // session data message
            var newClient = _clientPool.Clients[message.Id];
            _map.Rules.Map(out var rules);
            var response = new ConnectToSessionResponse()
            {
                Count = _map.Entities.Count,
                Rules = rules,
            };
            newClient.Send(new ByteArr(response).ToByteArray());
            foreach (var entity in _map.Entities.Values)
            {
                var points = 0;
                entity.Pos.Map(out var pos);
                if (entity is Food foodEntity)
                {
                    points = foodEntity.Points;
                    foodEntity.Color.Map(out var color);
                    var foodData = new FoodData()
                    {
                        Color = color,
                        Id = foodEntity.Id
                    };
                    newClient.Send(new ByteArr(foodData).ToByteArray());
                }
                else if (entity is Player playerEntity)
                {
                    points = playerEntity.Points;
                    playerEntity.Color.Map(out var color);
                    var playerData = new PlayerData()
                    {
                        Color = color,
                        Name = playerEntity.Name,
                        Id = playerEntity.Id
                    };
                    newClient.Send(new ByteArr(playerData).ToByteArray());
                }
                var state = new EntityUpdatedState()
                {
                    Id = entity.Id,
                    Points = points,
                    Pos = pos
                };
                newClient.Send(new ByteArr(state).ToByteArray());
            }
            _map.TryAddEntity(player);
        }
        catch (Exception ex) 
        {
            _logger?.Error(ex);
            throw;
        }
    }
}
