﻿using Google.Protobuf;

namespace Medio.Proto.Exceptions;

public class InvalidRequestException : Exception
{
    public IMessage Request { get; init; }
    public InvalidRequestException(IMessage request, string? message) : base(message)
    {
        Request = request;
    }
}
