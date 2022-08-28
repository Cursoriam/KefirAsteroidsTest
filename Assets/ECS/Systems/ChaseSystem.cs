using System;

public class ChaseSystem : ISystem
{
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var chasingComponent = ComponentManager.Instance.GetComponent<ChasingComponent>(entity.EntityId);
            if (!(chasingComponent is {Target: { }})) continue;
            var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
            var targetTransformComponent =
                ComponentManager.Instance.GetComponent<TransformComponent>(chasingComponent.Target.EntityId);

            if (transformComponent == null || targetTransformComponent == null) continue;
            var rotatingComponent = ComponentManager.Instance
                .GetComponent<RotatingComponent>(entity.EntityId);

            if (rotatingComponent != null)
            {
                rotatingComponent.RotationAngle = Utilities.RadiansToDegrees((float)Math.Atan2(
                    targetTransformComponent.Position.Y - transformComponent.Position.Y, 
                    targetTransformComponent.Position.X - transformComponent.Position.X));
            }
        }
    }
}
