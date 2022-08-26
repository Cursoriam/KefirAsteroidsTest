using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSystem : ISystem
{
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var chasableComponent = ComponentManager.Instance.GetComponent<ChasableComponent>(entity.EntityId);
            if (!(chasableComponent is {Target: { }})) continue;
            var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
            var targetTransformComponent =
                ComponentManager.Instance.GetComponent<TransformComponent>(chasableComponent.Target.EntityId);

            if (transformComponent == null || targetTransformComponent == null) continue;
            var roatationableComponent = ComponentManager.Instance
                .GetComponent<RotationableComponent>(entity.EntityId);

            if (roatationableComponent != null)
            {
                roatationableComponent.rotationAngle = Utilities.RadiansToDegrees((float)Math.Atan2(
                    targetTransformComponent.Position.Y - transformComponent.Position.Y, 
                    targetTransformComponent.Position.X - transformComponent.Position.X));
            }
        }
    }
}
