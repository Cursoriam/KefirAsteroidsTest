using System.Collections.Generic;

public interface IEntity
{
    public void AddComponent(IComponent component);
    public void RemoveComponent<T>();
    public T GetComponent<T>() where T: IComponent;
}
