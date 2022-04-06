/*using Medio.Domain;
using Medio.Domain.Entities;
using Medio.Domain.EntityCollisionHandlers;
using Medio.Messages;
using Medio.Network.ClientAcceptors;
using Medio.Network.ClientPools;
using Medio.PvPSession;
using Medio.PvPSession.ClientHandlers;
using Medio.PvPSession.MessageHandlers;
using Medio.PvPSession.Utilities;
using Medio.Session.Client.MessageHandlers;
using Medio.Session.Client.Utilities;
using System.Net;

IPEndPoint localAddr = new IPEndPoint(IPAddress.Parse("192.168.110.70"), 5000);

var coolHandlerManager = new EntityCollisionHandlerManager();
var map = new Map(Medio.Domain.Rules.GetDefaultPvPRules(), coolHandlerManager);
coolHandlerManager.RegisterHandler(new PlayerAndFoodCollisionHandler(map));

var acceptor = new MedioClientAcceptor(localAddr);
var pool = new ClientPool();
var messageHandlerManager = new MessageHandlerManager();
var moveRequestHandler = new MoveRequestHandler(map);
var spawnRequestHandler = new SpawnRequestHandler(map);
var sessionDataRequestHandler = new SessionDataRequestHandler(pool, map);
var clientHandlerCreator = new MedioPvPClientHandlerCreator(messageHandlerManager);
messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<MoveRequest>(), moveRequestHandler);
messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<SpawnRequest>(), spawnRequestHandler);
messageHandlerManager.RegisterHandler(MessageTypeIdManager.GetMessageTypeId<SessionDataRequest>(), sessionDataRequestHandler);
bool accepting = true;
acceptor.Start();
var task = Task.Run(() =>
{
    while (accepting)
    {
        var client = acceptor.Accept();
        // accepted
        pool.AddClient(client, clientHandlerCreator);
    }
    acceptor.Stop();
});
//Timer timer = new(Callback,null,0,100);
Console.ReadKey();
accepting = false;
task.Wait();

void Callback(object o)
{
    var entities = map.Entities;
    foreach (var id in map.UpdatedEntities)
    {
        var updated = new EntityUpdatedState()
        {
            Id = entities[id].Id,
            Pos = entities[id].Pos.GetDtoPos(),
            Points = entities[id].Points
        };
        foreach (var client in pool.Clients)
            client.Value.Send(new ByteArr(updated).ToByteArray());
    }
    map.UpdatedEntities.Clear();
    Console.WriteLine("updating");
}

class PlayerAndFoodCollisionHandler : EntityCollisionHandlerBase<Player, Food>
{
    private readonly MapImpl _map;
    public PlayerAndFoodCollisionHandler(MapImpl map)
    {
        _map = map;
    }
    protected override IEnumerable<IReadOnlyEntity> HandleCollision(Player entity, Food collider)
    {
        if (entity.Points / (float)collider.Points < _map.Rules.CanEatSizeDifference)
            return new List<IReadOnlyEntity>();

        entity.Points += collider.Points;
        collider.Points = 0;
        return new List<IReadOnlyEntity> { entity, collider };
    }
}*/