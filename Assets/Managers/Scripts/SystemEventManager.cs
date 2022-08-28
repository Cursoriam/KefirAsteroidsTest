using System;
using System.Collections.Generic;

public class SystemEventManager
{
    private static SystemEventManager _systemEventManager;
    private readonly Dictionary<string, Action> _eventDictionary;

    private SystemEventManager()
    {
        _eventDictionary = new Dictionary<string, Action>();
    }
    
    public static SystemEventManager Instance
    {
        get { return _systemEventManager ??= new SystemEventManager(); }
    }

    public void Subscribe(string eventName, Action listener)
    {
        if (_eventDictionary.ContainsKey(eventName))
        {
            _eventDictionary[eventName] += listener;
        }
        else
        {
            _eventDictionary.Add(eventName, listener);
        }
    }

    public void Unsubscribe(string eventName, Action listener)
    {
        if (_eventDictionary.ContainsKey(eventName))
            _eventDictionary[eventName] -= listener;
    }

    public void Trigger(string eventName)
    {
        if(_eventDictionary.ContainsKey(eventName))
            _eventDictionary[eventName]?.Invoke();
    }
}
