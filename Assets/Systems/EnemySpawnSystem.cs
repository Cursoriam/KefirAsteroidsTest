using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemySpawnSystem : ISystem
{
    private float _asteroidsTimeElapsed;
    private float _ufoTimeElapsed;
    public Action<string> BigAsteroidCreated;
    public Action<string, Coordinates2D> LittleAsteroidCreated;
    public Action UfoCreated;
    
    // Update is called once per frame
    public void Update()
    {
        var playerOnScene = false;
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var intersectionComponent = ComponentManager.Instance.GetComponent<IntersectionComponent>(entity.EntityId);
            var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);
            
            if (intersectionComponent is {Intersecting: { }})
            {
                var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
                var projectileTransformComponent =
                    ComponentManager.Instance.GetComponent<TransformComponent>(intersectionComponent.Intersecting
                        .EntityId);
                var firstSpawnPoint = new Coordinates2D(transformComponent.Position.X + 
                                                   projectileTransformComponent.Size.X / Constants.FloatTwo,
                    transformComponent.Position.Y + projectileTransformComponent.Size.Y / Constants.FloatTwo);
                var secondSpawnPoint = new Coordinates2D(transformComponent.Position.X -
                                                         projectileTransformComponent.Size.X / Constants.FloatTwo,
                    transformComponent.Position.Y - projectileTransformComponent.Size.Y / Constants.FloatTwo);
                CreateLittleAsteroid(firstSpawnPoint);
                CreateLittleAsteroid(secondSpawnPoint );
            }

            if (playerComponent != null)
            {
                playerOnScene = true;
            }
        }

        if (!playerOnScene) return;
        _asteroidsTimeElapsed += Constants.UpdateStep;
        _ufoTimeElapsed += Constants.UpdateStep;

        if (_asteroidsTimeElapsed > Constants.CoolDownBetweenAsteroidSpawn)
        {
            if (AsteroidsNumber() < Constants.MaxAsteroidsOnScene)
            {
                CreateBigAsteroid();
            }

            _asteroidsTimeElapsed = Constants.FloatZero;
        }

        if (_ufoTimeElapsed <= Constants.CoolDownBetweenUfoSpawn) return;
        if (!UFOIsOnScene())
        {
            EntityManager.Instance.CreateEntity<UFOEntity>(Constants.UfoEntityName);
            SetPosition(EntityManager.Instance.GetEntity(Constants.UfoEntityName));
            UfoCreated?.Invoke();
        }

        _ufoTimeElapsed = Constants.FloatZero;
    }

    private int AsteroidsNumber()
    {
        return EntityManager.Instance.GetAll().OfType<AsteroidEntity>().Count();
    }
    
    private bool UFOIsOnScene()
    {
        return EntityManager.Instance.GetAll().OfType<UFOEntity>().Any();
    }

    private void SetPosition(Entity entity)
    {
        var playerEntity = EntityManager.Instance.GetEntity(Constants.PlayerEntityName);
        var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
        if (transformComponent == null) return;
        while (SystemManager.Instance.GetSystem<InterSectionSystem>().InterSectsWith(entity, playerEntity))
        {
            transformComponent.Position = Utilities.CalculateEnemySpawnPosition();
        }
    }

    private int CalculateNumberToCreateAsteroid()
    {
        var totalAsteroidsNumber = AsteroidsNumber();
        for (var asteroidNumber = 1; asteroidNumber <= totalAsteroidsNumber; asteroidNumber++)
        {
            if (EntityManager.Instance.GetEntity(Constants.AsteroidEntityName + asteroidNumber) == null)
                return asteroidNumber;
        }
        return totalAsteroidsNumber + 1;
    }

    private void CreateBigAsteroid()
    {
        var newAsteroidName = Constants.AsteroidEntityName + CalculateNumberToCreateAsteroid();
        EntityManager.Instance.CreateEntity<BigAsteroidEntity>(newAsteroidName);
        SetPosition(EntityManager.Instance.GetEntity(newAsteroidName));
        BigAsteroidCreated?.Invoke(newAsteroidName);
    }

    private void CreateLittleAsteroid(Coordinates2D position)
    {
        var newAsteroidName = Constants.AsteroidEntityName + CalculateNumberToCreateAsteroid();
        EntityManager.Instance.CreateEntity<LittleAsteroidEntity>(newAsteroidName);

        var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(newAsteroidName);

        if (transformComponent != null)
        {
            transformComponent.Position = position;
        }
        LittleAsteroidCreated?.Invoke(newAsteroidName, position);
    }
}
