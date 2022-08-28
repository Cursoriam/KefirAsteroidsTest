using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySystem : ISystem
{
    public Action<float> PlayerVelocityChangedAction;
    public VelocitySystem()
    {
            SystemEventManager.Instance.Subscribe(Constants.AccelerateAction, Accelerate);
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var velocityComponent = ComponentManager.Instance.GetComponent<VelocityComponent>(entity.EntityId);
            if (velocityComponent == null) continue;
            var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);
            if (!Utilities.IsNegative(velocityComponent.Velocity + velocityComponent.Acceleration))
            {
                if (playerComponent != null)
                {
                    if (velocityComponent.Velocity + velocityComponent.Acceleration <= Constants.MaxPlayerVelocity)
                    {
                        velocityComponent.Velocity += velocityComponent.Acceleration;
                    }
                }
                else
                {
                    velocityComponent.Velocity += velocityComponent.Acceleration;
                }
            }
            else
                velocityComponent.Velocity = 0;

            if (playerComponent != null)
            {
                PlayerVelocityChangedAction?.Invoke(velocityComponent.Velocity);
            }
            
            if (!Utilities.IsNegative(velocityComponent.Acceleration))
            {
                velocityComponent.Acceleration = -velocityComponent.Acceleration;
            }
        }
    }

    private void Accelerate()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var velocityComponent = ComponentManager.Instance.GetComponent<VelocityComponent>(entity.EntityId);
            if (velocityComponent == null) continue;
            if (Utilities.IsNegative(velocityComponent.Acceleration))
            {
                velocityComponent.Acceleration = -velocityComponent.Acceleration;
            }
        }
    }
}
