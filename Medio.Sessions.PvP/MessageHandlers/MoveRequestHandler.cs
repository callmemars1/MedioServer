using Medio.Messages;
using Medio.Network.MessageHandlers;
using Medio.Domain;
using Medio.Domain.Utilities;
using Medio.Domain.Entities;

namespace Medio.PvPSession
{
    public class MoveRequestHandler : MessageHandlerBase<MoveRequest>
    {
        private readonly Map _map;

        public MoveRequestHandler(Map map)
        {
            _map = map;
        }
        protected override void Process(MoveRequest message)
        {
            // shit code

            _map.Entities[message.Id].Pos.X = message.Pos.X;
            _map.Entities[message.Id].Pos.Y = message.Pos.Y;
            _map.UpdateEntityState(message.Id, _map.Entities[message.Id] as Entity);
        }
    }
}
