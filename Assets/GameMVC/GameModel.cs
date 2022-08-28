using System;

public class GameModel
{
    public Action<string, Coordinates2D, Coordinates2D, float> ChangedTransformOfObjectAction;
    public Action<string> ObjectDestroyedAction;
    public Action<string, Coordinates2D, float> CreatedBigAsteroidAction;
    public Action<string, Coordinates2D, float> CreatedLittleAsteroidAction;
    public Action<Coordinates2D> CreatedUfoAction;
    public Action<string, Coordinates2D, float> BulletEntityCreatedAction;
    public Action<Coordinates2D, float> LaserCreatedAction;
    public Action CreatedPlayerAction;
    public Action<int> LaserNumberOfShootsChangedAction;
    public Action<float> LaserReloadTimeChangedAction;
    public Action<int> SendScoreAction;
    public Action GameOverAction;
    private int _score;
    private GameState _gameState = GameState.None;
    
    // Update is called once per frame
    public void Update()
    {
        if (_gameState == GameState.Running)
        {
            SystemManager.Instance.Update();
        }
    }

    private void EntityDestroyed(string name)
    {
        ObjectDestroyedAction?.Invoke(name);
    }

    private void CreatedBigAsteroid(string name)
    {
        var transform = ComponentManager.Instance.GetComponent<TransformComponent>(name);
        CreatedBigAsteroidAction?.Invoke(name, transform.Position, transform.Angle);
    }

    private void CreatedLittleAsteroid(string name, Coordinates2D position)
    {
        var transform = ComponentManager.Instance.GetComponent<TransformComponent>(name);
        CreatedLittleAsteroidAction?.Invoke(name, transform.Position, transform.Angle);
    }

    private void CreatedUfo(Coordinates2D position)
    {
        CreatedUfoAction?.Invoke(position);
    }

    private void CreatedBullet(string name, Coordinates2D position, float angle)
    {
        BulletEntityCreatedAction?.Invoke(name, position, angle);
    }

    private void CreatedLaser(Coordinates2D position, float angle)
    {
        LaserCreatedAction?.Invoke(position, angle);
    }

    private void TransformChanged(string name, Coordinates2D transform, Coordinates2D size, float angle)
    {
        ChangedTransformOfObjectAction?.Invoke(name, transform, size, angle);
    }
    
    private void LaserChargesCountChanged(int charges)
    {
        LaserNumberOfShootsChangedAction?.Invoke(charges);
    }

    private void LaserReloadTimeChanged(float reloadTime)
    {
        LaserReloadTimeChangedAction?.Invoke(reloadTime);
    }

    private void ScoreSent(int score)
    {
        _score += score;
        SendScoreAction?.Invoke(_score);
    }

    private void PlayerDestroyed()
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

        SystemManager.Instance.GetSystem<TransformSystem>().TransformChangedAction += TransformChanged;
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().BigAsteroidCreatedAction += CreatedBigAsteroid;
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().LittleAsteroidCreatedAction += CreatedLittleAsteroid;
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().UfoCreatedAction += CreatedUfo;
        SystemManager.Instance.GetSystem<ShootSystem>().BulletCreatedAction += CreatedBullet;
        SystemManager.Instance.GetSystem<LaserSystem>().LaserShotAction += CreatedLaser;
        SystemManager.Instance.GetSystem<DestructionSystem>().EntityDestroyedAction += EntityDestroyed;
        SystemManager.Instance.GetSystem<LaserSystem>().LaserChargesCountChangedAction += LaserChargesCountChanged;
        SystemManager.Instance.GetSystem<LaserSystem>().LaserReloadTimeChangedAction += LaserReloadTimeChanged;
        SystemManager.Instance.GetSystem<DestructionSystem>().PlayerDestroyedAction += PlayerDestroyed;
        SystemManager.Instance.GetSystem<DestructionSystem>().ScoreSentAction += ScoreSent;
        
        EntityManager.Instance.CreateEntity<PlayerEntity>(Constants.PlayerEntityName);
        CreatedPlayerAction?.Invoke();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        SystemManager.Instance.GetSystem<EnemySpawnSystem>().CreateBigAsteroid();
        _gameState = GameState.Running;
    }
}
