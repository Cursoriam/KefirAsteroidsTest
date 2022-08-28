using System;
using System.Linq;

public class ShootSystem : ISystem
{
    public Action<string, Coordinates2D, float> BulletCreatedAction;
    public ShootSystem()
    {
        SystemEventManager.Instance.Subscribe(Constants.ShootAction, Shoot);
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var bulletComponent = ComponentManager.Instance.GetComponent<BulletComponent>(entity.EntityId);
            if (bulletComponent == null) continue;
            bulletComponent.LifeTime += Constants.UpdateStep;
            if (bulletComponent.LifeTime <= Constants.BulletLifeTime) continue;
            
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
            BulletCreatedAction?.Invoke(newBulletName, bulletTransformComponent.Position, bulletTransformComponent.Angle);
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
