using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySystem : ISystem
{
    public VelocitySystem()
    {
            SystemEventManager.GetInstance().Subscribe("accelerate", Accelerate);
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.GetInstance().GetAllEntities())
        {
            var velocityComponent = ComponentManager.GetInstance().GetComponent<VelocityComponent>(entity.EntityId);
            if (velocityComponent != null)
            {
                if (velocityComponent.Velocity >= 0)
                    velocityComponent.Velocity += velocityComponent.Acceleration;
                else
                    velocityComponent.Velocity = 0;
                /*if (velocityComponent.Acceleration > 0)
                {
                    velocityComponent.Acceleration = -velocityComponent.Acceleration;
                }*/
            }
        }

        SystemEventManager.GetInstance().Trigger("move");
    }

    private void Accelerate()
    {
        foreach (var entity in EntityManager.GetInstance().GetAllEntities())
        {
            var velocityComponent = ComponentManager.GetInstance().GetComponent<VelocityComponent>(entity.EntityId);
            if (velocityComponent != null)
            {
                if(velocityComponent.Acceleration < 0)
                    ComponentManager.GetInstance().GetComponent<VelocityComponent>(entity.EntityId).Acceleration
                        = -velocityComponent.Acceleration;
            }
        }
    }
}
