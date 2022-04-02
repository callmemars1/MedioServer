using Medio.Messages;
using Medio.Network.MessageHandlers;
using Medio.Domain;
using Medio.Domain.Utilities;

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
            _map.TryUpdateEnityPos(message.Id, new Vector2D { X = message.Pos.X, Y = message.Pos.Y });
        }
    }
}
