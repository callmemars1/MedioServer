using Medio.Network.ClientPools;
using Medio.Network.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medio.Network.ClientHandlers
{
    public class PrintMessageHandler : ClientHandler
    {
        private readonly ClientPool pool;
        bool isRun = true;

        public PrintMessageHandler(Client client,ClientPool pool) : base(client)
        {
            this.pool = pool;
        }

        public override void StartHandle()
        {
            try
            {
                while (isRun)
                {
                    var buffer = new byte[256];
                    int bytes = _client.Receive(buffer, 0, buffer.Length);
                    Console.WriteLine($"{_client.RemoteEndPoint} said:{Encoding.UTF8.GetString(buffer, 0, bytes)}");
                    _client.Send(new Span<byte>(buffer, 0, bytes));
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(_client.Id + " disconnected");
                pool.RemoveClient(_client.Id);
            }
        }

        public override void StopHandle()
        {
           isRun = false;
        }
    }
}
