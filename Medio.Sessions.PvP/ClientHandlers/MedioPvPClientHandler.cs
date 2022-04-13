using Medio.Network.ClientHandlers;
using Medio.Network.Clients;
using Medio.Network.Exceptions;
using Medio.Proto;
using Medio.Proto.MessageHandlers;
using NLog;
using System.Collections.Concurrent;

namespace Medio.Sessions.PvP.ClientHandlers;

public class MedioPvPClientHandler : ClientHandler
{
    private readonly MessageHandlerManager _handlerManager;
    private readonly CancellationTokenSource _ctSource = new CancellationTokenSource();
    bool _handle = false;
    ILogger? _logger;
    public MedioPvPClientHandler(Client client, MessageHandlerManager handlerManager) : base(client)
    {
        _logger = LogManager.GetCurrentClassLogger();
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
            while (token.IsCancellationRequested == false && Client.Connected == true)
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
        catch (Exception ex)
        {
            _logger?.Error(ex.Message);
            StopHandle();
            throw new ClientConnectionInterruptedException(Client, "beda");
        }
        finally
        {
            StopHandle();
        }
    }

    public override void StopHandle()
    {
        if (_handle == false)
            return;

        _handle = false;
        _ctSource.Cancel();
        _logger?.Info($"{Client.Id}: handler stopped");
    }
}
