using UnityEngine;

public class TransformSystem : ISystem
{
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.GetInstance().GetAllEntities())
        {
            var transformComponent = ComponentManager.GetInstance().GetComponent<Transform>(entity.EntityId);
            var velocityComponent = ComponentManager.GetInstance().GetComponent<VelocityComponent>(entity.EntityId);
            if (velocityComponent != null && transformComponent != null)
            {
                transformComponent.Position.x += velocityComponent.Velocity;
                transformComponent.Position.y += velocityComponent.Velocity;
                SystemEventManager.GetInstance().Trigger("shipTransformChanged");
            }
        }
    }
}
