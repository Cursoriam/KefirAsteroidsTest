using System;
using UnityEngine;

public class TransformSystem : ISystem
{
    public Action<string, Coordinates2D, float> TransformChanged;
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
            var velocityComponent = ComponentManager.Instance.GetComponent<VelocityComponent>(entity.EntityId);
            
            if (velocityComponent != null && transformComponent != null)
            {
                var rotationableComponent =
                    ComponentManager.Instance.GetComponent<RotationableComponent>(entity.EntityId);
                if (rotationableComponent != null)
                {
                    transformComponent.Position.X += velocityComponent.Velocity * 
                                                     (float) Math.Cos(Utilities.DegreesToRadians(
                                                         rotationableComponent.rotationAngle));
                    transformComponent.Position.Y += velocityComponent.Velocity * 
                                                     (float) Math.Sin(Utilities.DegreesToRadians(
                                                         rotationableComponent.rotationAngle));
                }
                else
                {
                    transformComponent.Position.X += velocityComponent.Velocity *
                                                     (float) Math.Cos(Utilities.DegreesToRadians(
                                                         transformComponent.Angle));
                    transformComponent.Position.Y += velocityComponent.Velocity * 
                                                     (float) Math.Sin(Utilities.DegreesToRadians(
                                                         transformComponent.Angle));
                }
                RecalculateTransform(ref transformComponent);
                TransformChanged?.Invoke(entity.EntityId, transformComponent.Position, transformComponent.Angle);
            }
        }
    }

    private void RecalculateTransform(ref TransformComponent transformComponent)
    {
        if (transformComponent.Position.X > Constants.ScreenWidth + transformComponent.Size.X/Constants.FloatTwo)
            transformComponent.Position.X = -Constants.ScreenWidth - transformComponent.Size.X/Constants.FloatTwo;
        else if (transformComponent.Position.X < -Constants.ScreenWidth - transformComponent.Size.X/Constants.FloatTwo)
            transformComponent.Position.X = Constants.ScreenWidth + transformComponent.Size.X/Constants.FloatTwo;
        else if (transformComponent.Position.Y > Constants.ScreenHeight + transformComponent.Size.Y/Constants.FloatTwo)
            transformComponent.Position.Y = -Constants.ScreenHeight - transformComponent.Size.Y/Constants.FloatTwo;
        else if (transformComponent.Position.Y < -Constants.ScreenHeight - transformComponent.Size.Y/Constants.FloatTwo)
            transformComponent.Position.Y = Constants.ScreenHeight + transformComponent.Size.Y/Constants.FloatTwo;
    }
}
