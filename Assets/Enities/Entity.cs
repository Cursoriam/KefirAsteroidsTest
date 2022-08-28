using System.Collections.Generic;
using System.Linq;

public class Entity : IEntity
{
    public readonly string EntityId;
    protected readonly List<IComponent> Components;

    protected Entity()
    {
        Components = new List<IComponent>();
    }

    protected Entity(string entityId)
    {
        EntityId = entityId;
        Components = new List<IComponent>();
    }

    public void AddComponent(IComponent component)
    {
        Components.Add(component);
    }

    public void RemoveComponent<T>()
    {
        foreach (var component in Components.Where(component => component.GetType() == typeof(T)))
        {
            Components.Remove(component);
        }
    }

    public T GetComponent<T>() where T: IComponent
    {
        foreach (var component in Components.Where(component => component.GetType() == typeof(T)))
        {
            return (T)component;
        }

        return default;
    }
}
