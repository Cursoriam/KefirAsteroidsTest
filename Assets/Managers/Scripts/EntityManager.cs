using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EntityManager
{
    private static EntityManager _entityManager;
    private Dictionary<string, Entity> _entities;

    public static EntityManager GetInstance()
    {
        if (_entityManager == null)
            _entityManager = new EntityManager();
        return _entityManager;
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
        return _entities[entityId];
    }

    public IEnumerable<Entity> GetAllEntities()
    {
        List<Entity> entities = new List<Entity>();
        foreach (var entity in _entities)
        {
            entities.Add(entity.Value);
        }

        return entities;
    }
}