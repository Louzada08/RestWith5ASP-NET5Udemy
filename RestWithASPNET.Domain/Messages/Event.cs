﻿using MediatR;

namespace RestWithASPNET.Domain.Messages;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    public Event()
    {
        Timestamp = DateTime.Now;
    }
}
