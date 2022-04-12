using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using Medio.Network.Exceptions;
using Medio.Proto;
using Medio.Proto.MessageHandlers;
using System.Collections.Concurrent;

namespace Medio.Sessions.PvP.ClientHandlers;

public class MedioPvPClientHandler : ClientHandler
{
    private readonly MessageHandlerManager _handlerManager;
    private readonly CancellationTokenSource _ctSource = new CancellationTokenSource();
    bool _handle = false;
    public MedioPvPClientHandler(Client client, MessageHandlerManager handlerManager) : base(client)
    {
        _handlerManager = handlerManager;
    }

    public override bool Active { get; protected set; }

    public override void StartHandle()
    {
        try
        {
            if (_handle == true)
                return;

            if (Client.Connected == false)
                throw new ArgumentException("client not connected");

            var token = _ctSource.Token;
            _handle = true;
            while (token.IsCancellationRequested == false)
            {
                var msgSizeBytes = Client.Receive(ByteArr.SizeBytesCount); // read size part of header
                var msgSize = BitConverter.ToInt32(msgSizeBytes); // get int from readed bytes
                var msgBytes = Client.Receive(msgSize - ByteArr.SizeBytesCount); // read full msg
                // concat two readed arrays for right deserialize
                var fullMsgBytes = new byte[msgSize];
                msgSizeBytes.CopyTo(fullMsgBytes.AsSpan(0, ByteArr.SizeBytesCount));
                msgBytes.CopyTo(fullMsgBytes.AsSpan(ByteArr.SizeBytesCount, msgSize - ByteArr.SizeBytesCount));
                Task.Run(() =>
                {
                    _handlerManager.Handle(fullMsgBytes);
                });
            }
        }
        catch (Exception)
        {
            _handle = false;
            Client.Close();
            StopHandle();

            throw new ClientConnectionInterruptedException(Client, "beda");
        }
    }

    public override void StopHandle()
    {
        _handle = false;
        _ctSource.Cancel();
    }
}
