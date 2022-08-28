using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Action<string, Coordinates2D, Coordinates2D, float> ChangedTransformOfObjectAction;
    public Action<string> DestroyObjectAction;
    public Action<string, Coordinates2D, float> CreatedBigAsteroidAction;
    public Action<string, Coordinates2D, float> CreatedLittleAsteroidAction;
    public Action<Coordinates2D> CreatedUfoAction;
    public Action<string, Coordinates2D, float> BulletEntityCreated;
    public Action<Coordinates2D, float> CreateLaserAction;
    public Action CreatedPlayerAction;
    public Action<int> LaserNumberOfShootsChangedAction;
    public Action<float> LaserReloadTimeChangedAction;
    public Action<int> SendScoreAction;
    public Action GameOverAction;
    private int _score = 0;
    private GameState _gameState = GameState.None;
    
    // Update is called once per frame
    public void Update()
    {
        if (_gameState == GameState.Running)
        {
            SystemManager.Instance.Update();
        }
    }

    public void DestroyObjectOnScene(string name)
    {
        DestroyObjectAction?.Invoke(name);
    }

    public void CreatedBigAsteroid(string name)
    {
        var transform = ComponentManager.Instance.GetComponent<TransformComponent>(name);
        CreatedBigAsteroidAction?.Invoke(name, transform.Position, transform.Angle);
    }

    public void CreateLittleAsteroid(string name, Coordinates2D position)
    {
        var transform = ComponentManager.Instance.GetComponent<TransformComponent>(name);
        CreatedLittleAsteroidAction?.Invoke(name, transform.Position, transform.Angle);
    }

    public void CreatedUFO(Coordinates2D position)
    {
        CreatedUfoAction?.Invoke(position);
    }
    
    public void CreateBullet(string name, Coordinates2D position, float angle)
    {
        BulletEntityCreated?.Invoke(name, position, angle);
    }

    public void CreateLaser(Coordinates2D position, float angle)
    {
        CreateLaserAction?.Invoke(position, angle);
    }

    private void ChangeTransformOfObject(string name, Coordinates2D transform, Coordinates2D size, float angle)
    {
        ChangedTransformOfObjectAction?.Invoke(name, transform, size, angle);
    }
    
    private void LaserNumberOfChargesChanged(int charges)
    {
        LaserNumberOfShootsChangedAction?.Invoke(charges);
    }

    private void LaserReloadTimeChanged(float reloadTime)
    {
        LaserReloadTimeChangedAction?.Invoke(reloadTime);
    }

    private void SendScore(int score)
    {
        _score += score;
        SendScoreAction?.Invoke(_score);
    }

    private void GameOver()
    {
        GameOverAction?.Invoke();
    }

    public void RestartGame()
    {
        SystemManager.Instance.GetSystem<DestructionSystem>().DestroyAllEntities();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().ResetVariables();
        SystemManager.Instance.GetSystem<LaserSystem>().ResetVariables();
        _score = Constants.IntZero;

        EntityManager.Instance.CreateEntity<PlayerEntity>(Constants.PlayerEntityName);
        CreatedPlayerAction?.Invoke();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
    }

    public void StartGame()
    {
        SystemManager.Instance.AddSystem(new InputSystem());
        SystemManager.Instance.AddSystem(new VelocitySystem());
        SystemManager.Instance.AddSystem(new RotationSystem());
        SystemManager.Instance.AddSystem(new TransformSystem());
        SystemManager.Instance.AddSystem(new ShootSystem());
        SystemManager.Instance.AddSystem(new LaserSystem());
        SystemManager.Instance.AddSystem(new InterSectionSystem());
        SystemManager.Instance.AddSystem(new EnemySpawnSystem());
        SystemManager.Instance.AddSystem(new ChaseSystem());
        SystemManager.Instance.AddSystem(new DestructionSystem());

        SystemManager.Instance.GetSystem<TransformSystem>().TransformChanged += ChangeTransformOfObject;
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().BigAsteroidCreated += CreatedBigAsteroid;
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().LittleAsteroidCreated += CreateLittleAsteroid;
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().UfoCreated += CreatedUFO;
        SystemManager.Instance.GetSystem<ShootSystem>().CreateBullet += CreateBullet;
        SystemManager.Instance.GetSystem<LaserSystem>().LaserShot += CreateLaser;
        SystemManager.Instance.GetSystem<DestructionSystem>().DestroyEntity += DestroyObjectOnScene;
        SystemManager.Instance.GetSystem<LaserSystem>().LaserChargesCountChanged += LaserNumberOfChargesChanged;
        SystemManager.Instance.GetSystem<LaserSystem>().LaserReloadTimeChanged += LaserReloadTimeChanged;
        SystemManager.Instance.GetSystem<DestructionSystem>().PlayerDestroyedAction += GameOver;
        SystemManager.Instance.GetSystem<DestructionSystem>().SendScore += SendScore;
        EntityManager.Instance.CreateEntity<PlayerEntity>(Constants.PlayerEntityName);
        CreatedPlayerAction?.Invoke();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        _gameState = GameState.Running;
    }
}
