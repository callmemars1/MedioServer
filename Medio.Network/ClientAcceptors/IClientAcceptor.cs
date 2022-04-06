using Medio.Network.Clients;

namespace Medio.Network.ClientAcceptors;



/*    
 * ClientAcceptor'ы отвечают за обработку входящих соединений
*/
public interface IClientAcceptor
{
    public Client Accept();
    public void Start();
    public void Stop();
    public bool Accepting { get; }
}
