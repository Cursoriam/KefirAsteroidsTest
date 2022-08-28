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
        return entity != null ? EntityManager.Instance.GetEntity(entityId).GetComponent<T>() : default;
    }
}
