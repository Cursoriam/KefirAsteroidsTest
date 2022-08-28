using System;
using System.Collections.Generic;

public class EntityManager
{
    private static EntityManager _entityManager;
    private readonly Dictionary<string, Entity> _entities;
    
    public static EntityManager Instance
    {
        get { return _entityManager ??= new EntityManager(); }
    }

    private EntityManager()
    {
        _entities = new Dictionary<string, Entity>();
    }

    public void CreateEntity<T>(string entityId) where T: Entity, new()
    {
        _entities.Add(entityId, (Entity)Activator.CreateInstance(typeof(T), entityId));
    }

    public void RemoveEntity(string entityId)
    {
        _entities.Remove(entityId);
    }

    public Entity GetEntity(string entityId)
    {
        if(_entities.ContainsKey(entityId))
            return _entities[entityId];
        return null;
    }

    public IEnumerable<Entity> GetAll()
    {
        List<Entity> entities = new List<Entity>();
        foreach (var entity in _entities)
        {
            entities.Add(entity.Value);
        }

        return entities;
    }
}
