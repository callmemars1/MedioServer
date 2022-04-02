using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medio.Network.MessageHandlers
{
    public interface IMessageHandler
    {
        void Handle(byte[] message);
    }
}
