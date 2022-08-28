public class UfoEntity : Entity
{
    public UfoEntity() {}

    public UfoEntity(string entityId) : base(entityId)
    {
        Components.Add(new TransformComponent{Position = Utilities.CalculateEnemySpawnPosition(),
            Size = Constants.UfoSize});
        Components.Add(new VelocityComponent{Velocity = Constants.UfoVelocity});
        Components.Add(new EnemyComponent{Score = Constants.UfoScore});
        Components.Add(new ChasingComponent{Target = EntityManager.Instance.
            GetEntity(Constants.PlayerEntityName)});
        Components.Add(new DestructibleComponent());
        Components.Add(new RotatingComponent());
    }
}
