using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserSystem : ISystem
{
    private int _numberOfShoots = Constants.NumberOfLaserShoots;
    private float _coolDownTime;
    public Action<Coordinates2D, float> LaserShot;
    public LaserSystem()
    {
        SystemEventManager.Instance.Subscribe(Constants.LaserAction, LaserShoot); 
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var laserComponent = ComponentManager.Instance.GetComponent<LaserComponent>(entity.EntityId);
            if (laserComponent != null)
            {
                laserComponent.LifeTime += Constants.UpdateStep;
                if (laserComponent.LifeTime > Constants.LaserLifeTime)
                {
                    var destructibleComponent =
                        ComponentManager.Instance.GetComponent<DestructibleComponent>(entity.EntityId);
                    if (destructibleComponent != null)
                    {
                        destructibleComponent.NeedToDestroy = true;
                    }
                }
            }

            if (_numberOfShoots >= Constants.NumberOfLaserShoots) continue;
            if (_coolDownTime < Constants.LaserReloadTime)
            {
                _coolDownTime += Constants.UpdateStep;
            }
            else
            {
                _numberOfShoots++;
                _coolDownTime = Constants.FloatZero;
            }
        }
    }

    private void LaserShoot()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);

            if (playerComponent == null) continue;
            if (_numberOfShoots <= Constants.IntZero || LaserAlreadyExists()) continue;
            EntityManager.Instance.CreateEntity<LaserEntity>(Constants.LaserName);
            var playerTransformComponent =
                ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
            var laserTransformComponent =
                ComponentManager.Instance.GetComponent<TransformComponent>(Constants.LaserName);
            if (playerTransformComponent == null || laserTransformComponent == null) continue;
            laserTransformComponent.Position = playerTransformComponent.Position;
            laserTransformComponent.Angle = playerTransformComponent.Angle;
            laserTransformComponent.Position.X += laserTransformComponent.Size.X / Constants.FloatTwo;
            laserTransformComponent.Position.Y += laserTransformComponent.Size.Y / Constants.FloatTwo;
            laserTransformComponent.Position = Utilities.RotatePoint(laserTransformComponent.Position,
                playerTransformComponent.Position, laserTransformComponent.Angle);
            _numberOfShoots--;
            LaserShot?.Invoke(laserTransformComponent.Position, laserTransformComponent.Angle);
        }
    }

    private bool LaserAlreadyExists()
    {
        return EntityManager.Instance.GetAll().Select(entity => 
            ComponentManager.Instance.GetComponent<LaserComponent>(entity.EntityId)).Any(laserComponent
            => laserComponent != null);
    }
}
