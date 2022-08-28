using System;
using System.Collections.Generic;

public class InterSectionSystem : ISystem
{
    public Action<Coordinates2D> CreateLittleAsteroid;
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var enemyComponent = ComponentManager.Instance.GetComponent<EnemyComponent>(entity.EntityId);
            if (enemyComponent != null)
            {
                foreach (var otherEntity in EntityManager.Instance.GetAll())
                {
                    var bulletComponent = ComponentManager.Instance
                        .GetComponent<BulletComponent>(otherEntity.EntityId);
                    var laserComponent = ComponentManager.Instance
                        .GetComponent<LaserComponent>(otherEntity.EntityId);
                    var playerComponent = ComponentManager.Instance
                        .GetComponent<PlayerComponent>(otherEntity.EntityId);

                    if (bulletComponent != null)
                    {
                        if (InterSectsWith(entity, otherEntity))
                        {
                            var intersectionComponent =
                                ComponentManager.Instance.GetComponent<IntersectionComponent>(entity.EntityId);

                            if (intersectionComponent != null)
                            {
                                intersectionComponent.Intersecting = otherEntity;
                            }
                            
                            var enemyDestructibleComponent =
                                ComponentManager.Instance.GetComponent<DestructibleComponent>(entity.EntityId);

                            if (enemyDestructibleComponent != null)
                            {
                                enemyDestructibleComponent.NeedToDestroy = true;
                            }

                            var projectileDestructibleComponent =
                                ComponentManager.Instance.GetComponent<DestructibleComponent>(otherEntity.EntityId);

                            if (projectileDestructibleComponent != null)
                            {
                                projectileDestructibleComponent.NeedToDestroy = true;
                            }
                        }
                    }

                    if (laserComponent != null)
                    {
                        if (InterSectsWith(entity, otherEntity))
                        {
                            var intersectionComponent =
                                ComponentManager.Instance.GetComponent<IntersectionComponent>(entity.EntityId);

                            if (intersectionComponent != null)
                            {
                                intersectionComponent.Intersecting = otherEntity;
                            }
                            
                            var enemyDestructibleComponent =
                                ComponentManager.Instance.GetComponent<DestructibleComponent>(entity.EntityId);

                            if (enemyDestructibleComponent != null)
                            {
                                enemyDestructibleComponent.NeedToDestroy = true;
                            }
                        }
                    }

                    if (playerComponent != null)
                    {
                        if (InterSectsWith(entity, otherEntity))
                        {
                            var playerDestructibleComponent =
                                ComponentManager.Instance.GetComponent<DestructibleComponent>(otherEntity.EntityId);

                            if (playerDestructibleComponent != null)
                            {
                                playerDestructibleComponent.NeedToDestroy = true;
                            }

                            var chasingComponent =
                                ComponentManager.Instance.GetComponent<ChasingComponent>(entity.EntityId);

                            var enemyDestructibleComponent =
                                ComponentManager.Instance.GetComponent<DestructibleComponent>(entity.EntityId);

                            if (chasingComponent != null && enemyDestructibleComponent != null)
                            {
                                enemyDestructibleComponent.NeedToDestroy = true;
                            }
                        }
                    }
                }
            }
        }
    }
    
    public bool InterSectsWith(Entity firstEntity, Entity secondEntity)
    {
        var firstEntityTransform =
            ComponentManager.Instance.GetComponent<TransformComponent>(firstEntity.EntityId);
        var secondEntityTransform = ComponentManager.Instance.
            GetComponent<TransformComponent>(secondEntity.EntityId);


        List<Coordinates2D> firstEntityVertexes = Utilities.GetPrimitiveConvexHull(firstEntityTransform.Position,
            firstEntityTransform.Size, firstEntityTransform.Angle);


        List<Coordinates2D> secondEntityVertexes = Utilities.GetPrimitiveConvexHull(secondEntityTransform.Position,
            secondEntityTransform.Size, secondEntityTransform.Angle);

        return Utilities.IsPolygonsIntersecting(firstEntityVertexes, secondEntityVertexes);
    }
}
