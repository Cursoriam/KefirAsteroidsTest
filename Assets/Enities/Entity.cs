using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : IEntity
{
    public string EntityId;
    protected List<IComponent> _components;

    public Entity()
    {
        _components = new List<IComponent>();
    }
    
    public Entity(string entityId)
    {
        EntityId = entityId;
        _components = new List<IComponent>();
    }

    public void AddComponent(IComponent component)
    {
        _components.Add(component);
    }

    public void RemoveComponent<T>()
    {
        foreach (var component in _components)
        {
            if (component.GetType() == typeof(T))
                _components.Remove(component);
        }
    }

    public T GetComponent<T>() where T: IComponent
    {
        foreach (var component in _components)
        {
            if (component.GetType() == typeof(T))
                return (T)component;
        }
        return default;
    }
}
