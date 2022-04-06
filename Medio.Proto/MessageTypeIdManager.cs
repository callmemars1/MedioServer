using Newtonsoft.Json.Linq;
using Google.Protobuf;

namespace Medio.Proto;

public class MessageTypeIdManager
{
    static MessageTypeIdManager()
    {
        string contents = File.ReadAllText(@"MessageTypeIds.json");
        _ids = JObject.Parse(contents);
    }
    private static readonly JObject _ids;
    public static int GetMessageTypeId<T>()
        where T : IMessage
    {
        var token = _ids[typeof(T).Name];
        if (token == null)
            throw new ArgumentException("no id for this type!");

        return token.Value<int>();
    }
    public static int GetMessageTypeId<T>(T obj)
        where T : notnull, IMessage
    {
        var token = _ids[obj.GetType().Name];
        if (token == null)
            throw new ArgumentException("no id for this type!");

        return token.Value<int>();
    }
}
