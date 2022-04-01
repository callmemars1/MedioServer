using Medio.Network.ClientPools;
using Medio.Network.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medio.Network.ClientHandlers;
public class PrintMessageHandlerCreator : IClientHandlerCreator
{
    private readonly ClientPool pool;

    public PrintMessageHandlerCreator(ClientPool pool)
    {
        this.pool = pool;
    }
    public ClientHandler Create(Client client)
    {
        return new PrintMessageHandler(client,pool);
    }
}
