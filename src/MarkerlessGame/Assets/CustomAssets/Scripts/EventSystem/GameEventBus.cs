using System.Collections.Generic;
using UnityEngine;
using Type = System.Type;
using Delegate = System.Delegate;
using System;

public class GameEventBus
{
    // Contains look-ups for events and ('combined') methods subscribed to the event
    private static readonly Dictionary<Type, Delegate> _eventTable = new Dictionary<Type, Delegate>();

    // When called, replaces generic type 'T' with a struct that extends IEventPayload
    // Takes a method (action) with argument of type 'T' that returns void
    public static void Subscribe<T>(Action<T> listener) where T : struct, IEventPayload
    {
        Type type = typeof(T);
        if (!_eventTable.ContainsKey(type))
        {
            _eventTable[type] = null;
        }
        _eventTable[type] = Delegate.Combine(_eventTable[type], listener);
    }

    // When called, replaces generic type 'T' with a struct that extends IEventPayload
    // Takes a method (action) with argument of type 'T' that returns void
    public static void Unsubcribe<T>(Action<T> listener) where T : struct, IEventPayload
    {
        Type type = typeof(T);
        if (_eventTable.ContainsKey(type))
        {
            _eventTable[type] = Delegate.Remove(_eventTable[type], listener);
        }
    }

    // When called, replaces generic type 'T' with a struct that extends IEventPayload
    // Accepts an argument of that specific struct
    public static void Raise<T>(T eventPayload) where T : struct, IEventPayload
    {
        Type type = typeof(T);
        if (_eventTable.TryGetValue(type, out Delegate d))
        {
            if (d is Action<T> callback)
            {
                callback.Invoke(eventPayload);
            }
        }
    }
}
