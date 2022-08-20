using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemManager
{
    private List<ISystem> _systems;
    private static SystemManager _systemManager;

    public static SystemManager GetInstance()
    {
        if (_systemManager == null)
            _systemManager = new SystemManager();
        return _systemManager;
    }

    private SystemManager()
    {
        _systems = new List<ISystem>();
    }

    public void AddSystem(ISystem system)
    {
        _systems.Add(system);
    }

    public T GetSystem<T>()
    {
        return (T)_systems.Where(item => item.GetType() == typeof(T)).ToList()[0];
    }
    
    public void RemoveSystem<T>()
    {
        _systems.Remove((ISystem)typeof(T));
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var system in _systems)
        {
            system.Update();
        }
    }
}
