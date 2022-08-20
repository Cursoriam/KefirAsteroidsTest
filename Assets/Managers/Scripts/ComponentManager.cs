using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ComponentManager
{
    private static ComponentManager _componentManager;

    public static ComponentManager GetInstance()
    {
        if (_componentManager == null)
            _componentManager = new ComponentManager();
        return _componentManager;
    }

    public void AddComponent<T>(string entityId) where T: IComponent, new()
    {
        EntityManager.GetInstance().GetEntity(entityId).AddComponent(new T());
    }

    public void RemoveComponent<T>(string entityId) where T: IComponent
    {
        EntityManager.GetInstance().GetEntity(entityId).RemoveComponent<T>();
    }

    public T GetComponent<T>(string entityId) where T: IComponent
    {
        return (T)EntityManager.GetInstance().GetEntity(entityId).GetComponent<T>();
    }
}
