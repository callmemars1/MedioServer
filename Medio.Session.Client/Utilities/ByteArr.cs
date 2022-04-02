using Google.Protobuf;

namespace Medio.Session.Client.Utilities;

public class ByteArr
{
    public const int MessageTypeIdBytesCount = 4;
    public const int SizeBytesCount = 4;
    public const int HeaderBytesCount = SizeBytesCount + MessageTypeIdBytesCount;
    public int MessageTypeId { get; private set; }
    public byte[] Data { get; private set; }
    public int Size { get; private set; } // full size of package (with "Bytes" size)
    public ByteArr(IMessage message)
    {
        MessageTypeId = MessageTypeIdManager.GetMessageTypeId(message);
        Data = message.ToByteArray();
        Size = HeaderBytesCount + Data.Length;
    }
    public ByteArr(byte[] data)
    {
        MessageTypeId = BitConverter.ToInt32(data, SizeBytesCount);
        Data = new Span<byte>(data, HeaderBytesCount, data.Length - HeaderBytesCount).ToArray();
        Size = data.Length;
    }

    public byte[] ToByteArray()
    {
        List<byte> bytes = new();
        bytes.AddRange(BitConverter.GetBytes(Size));
        bytes.AddRange(BitConverter.GetBytes(MessageTypeId));
        bytes.AddRange(Data);
        return bytes.ToArray();
    }
}
