using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootSystem : ISystem
{
    public Action<string, Coordinates2D, float> CreateBullet;
    public ShootSystem()
    {
        SystemEventManager.Instance.Subscribe(Constants.ShootAction, Shoot);
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var shootableComponent = ComponentManager.Instance.GetComponent<BulletComponent>(entity.EntityId);
            if (shootableComponent == null) continue;
            shootableComponent.LifeTime += Constants.UpdateStep;
            if (shootableComponent.LifeTime <= Constants.BulletLifeTime) continue;
            
            var destructibleComponent = ComponentManager.Instance.
                GetComponent<DestructibleComponent>(entity.EntityId);

            if (destructibleComponent != null)
            {
                destructibleComponent.NeedToDestroy = true;
            }
        }
    }

    private void Shoot()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);

            if (playerComponent == null) continue;
            var newBulletName = Constants.BulletName + CalculateNumberToCreateBullet();
            EntityManager.Instance.CreateEntity<BulletEntity>(newBulletName);
            var entityTransformComponent =
                ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
            var bulletTransformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(newBulletName);

            if (entityTransformComponent == null || bulletTransformComponent == null) continue;
            bulletTransformComponent.Position = entityTransformComponent.Position;
            bulletTransformComponent.Angle = entityTransformComponent.Angle;
            CreateBullet?.Invoke(newBulletName, bulletTransformComponent.Position, bulletTransformComponent.Angle);
        }
    }

    private int CalculateNumberOfBullets()
    {
        return EntityManager.Instance.GetAll().OfType<BulletEntity>().Count();
    }

    private int CalculateNumberToCreateBullet()
    {
        var totalBulletsNumber = CalculateNumberOfBullets();
        for (var bulletNumber = 1; bulletNumber <= totalBulletsNumber; bulletNumber++)
        {
            if (EntityManager.Instance.GetEntity(Constants.BulletName + bulletNumber) == null)
                return bulletNumber;
        }
        return totalBulletsNumber + 1;
    }
}
