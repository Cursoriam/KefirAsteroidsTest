using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionSystem : ISystem
{
    public Action<string> DestroyEntity;
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var destructibleComponent = ComponentManager.Instance.GetComponent<DestructibleComponent>(entity.EntityId);
            if (destructibleComponent is {NeedToDestroy: true})
            {
                DestroyEntity?.Invoke(entity.EntityId);
                EntityManager.Instance.RemoveEntity(entity.EntityId);
            }
        }
    }
}
