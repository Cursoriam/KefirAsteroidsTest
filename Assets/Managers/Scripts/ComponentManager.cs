using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ComponentManager
{
    private static ComponentManager _componentManager;
    
    public static ComponentManager Instance
    {
        get { return _componentManager ??= new ComponentManager(); }
    }

    public void AddComponent<T>(string entityId) where T: IComponent, new()
    {
        EntityManager.Instance.GetEntity(entityId).AddComponent(new T());
    }

    public void RemoveComponent<T>(string entityId) where T: IComponent
    {
        EntityManager.Instance.GetEntity(entityId).RemoveComponent<T>();
    }

    public T GetComponent<T>(string entityId) where T: IComponent
    {
        var entity = EntityManager.Instance.GetEntity(entityId);
        if(entity != null)
            return (T)EntityManager.Instance.GetEntity(entityId).GetComponent<T>();
        return default;
    }
}
