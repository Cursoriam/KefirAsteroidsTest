using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserSystem : ISystem
{
    public Action<Coordinates2D, float> LaserTransformChanged;
    public Action<int> LaserChargesCountChanged;
    public Action<float> LaserReloadTimeChanged;
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
                else
                {
                    RecalculateLaserTransform(entity.EntityId);
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
                LaserChargesCountChanged?.Invoke(_numberOfShoots);
                _coolDownTime = Constants.FloatZero;
            }
            LaserReloadTimeChanged?.Invoke(Constants.LaserReloadTime - _coolDownTime);
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
            LaserChargesCountChanged?.Invoke(_numberOfShoots);
            LaserShot?.Invoke(playerTransformComponent.Position, laserTransformComponent.Angle);
        }
    }

    private bool LaserAlreadyExists()
    {
        return EntityManager.Instance.GetAll().Select(entity => 
            ComponentManager.Instance.GetComponent<LaserComponent>(entity.EntityId)).Any(laserComponent
            => laserComponent != null);
    }

    private TransformComponent GetPlayerTransformComponent()
    {
        return (from entity in EntityManager.Instance.GetAll() 
            let playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId)
            where playerComponent != null select 
                ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId)).
            FirstOrDefault(transformComponent => transformComponent != null);
    }

    private void RecalculateLaserTransform(string entityId)
    {
        var laserTransformComponent =
            ComponentManager.Instance.GetComponent<TransformComponent>(entityId);
        if (laserTransformComponent != null)
        {
            var playerTransformComponent = GetPlayerTransformComponent();
            if (playerTransformComponent != null)
            {
                laserTransformComponent.Position = playerTransformComponent.Position;
                laserTransformComponent.Angle = playerTransformComponent.Angle;
                laserTransformComponent.Position.X += laserTransformComponent.Size.X / Constants.FloatTwo;
                laserTransformComponent.Position.Y += laserTransformComponent.Size.Y / Constants.FloatTwo;
                laserTransformComponent.Position = Utilities.RotatePoint(laserTransformComponent.Position,
                    playerTransformComponent.Position, laserTransformComponent.Angle);
                LaserTransformChanged?.Invoke(playerTransformComponent.Position, laserTransformComponent.Angle);
            }
        }
    }

    public void ResetVariables()
    {
        _numberOfShoots = Constants.NumberOfLaserShoots;
        _coolDownTime = Constants.FloatZero;
    }
}
