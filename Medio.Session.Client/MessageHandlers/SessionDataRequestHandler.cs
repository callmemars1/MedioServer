using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Messages;
using Medio.Network.ClientPools;
using Medio.Network.MessageHandlers;
using Medio.PvPSession.Exceptions;
using Medio.PvPSession.Utilities;
using Medio.Session.Client.Utilities;

namespace Medio.PvPSession.MessageHandlers;

public class SessionDataRequestHandler : MessageHandlerBase<SessionDataRequest>
{
    private Map _map;
    private ClientPool _clientPool;
    public SessionDataRequestHandler(ClientPool pool,Map map)
    {
        _map = map;
        _clientPool = pool;
    }
    protected override void Process(SessionDataRequest message)
    {
        if (_clientPool.Clients.ContainsKey(message.Id) == false)
            throw new InvalidRequestException(message, "no client with this id");

        var reply = new SessionDataResponse() { Rules = _map.Rules.GetDtoRules()};
        foreach (var entityPair in _map.Entities)
        {
            if (entityPair.Value is Player pl)
            {
                reply.PlayersData.Add(new PlayerData()
                {
                    Color = pl.Color.GetDtoColor(),
                    Id = pl.Id,
                    Name = pl.Name,
                    Type = pl.Type,
                });
            }
            else
            {
                reply.EntitiesData.Add(new EntityData()
                {
                    Color = entityPair.Value.Color.GetDtoColor(),
                    Id = entityPair.Key,
                    Type = entityPair.Value.Type,
                });
            }
            reply.LastActualStates.Add(new EntityUpdatedState()
            {
                Id = entityPair.Key,
                // извиняюсь за это....................
                Points = entityPair.Value.Points,
                Pos = entityPair.Value.Pos.GetDtoPos(),
            }) ;
        }
        _clientPool.Clients[message.Id].Send(new ByteArr(reply).ToByteArray());
        _map.AddEntity(new Player(message.Id, _map.Rules.SizeIncreaseCoefficient)
        {
            Color = message.PlayerData.Color.FromDtoColor(),
            Name = message.PlayerData.Name,
            Type = message.PlayerData.Type,
            Pos = new Domain.Utilities.Vector2D { X = -1, Y = -1 },
            Points = 0
        });
    }
}
