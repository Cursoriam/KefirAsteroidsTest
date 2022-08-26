using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySystem : ISystem
{
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
            if (!Utilities.IsNegative(velocityComponent.Velocity + velocityComponent.Acceleration))
                velocityComponent.Velocity += velocityComponent.Acceleration;
            else
                velocityComponent.Velocity = 0;
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
