using Random = System.Random;

public class BigAsteroidEntity : AsteroidEntity
{
    public BigAsteroidEntity(): base(){}

    public BigAsteroidEntity(string entityId) : base(entityId)
    {
        Random = new Random();
        Components.Add(new TransformComponent{Angle = Random.Next(Constants.IntZero,
                (int)Constants.PIInDegrees), Position = Utilities.CalculateEnemySpawnPosition(),
            Size = Constants.BigAsteroidSize});
        Components.Add(new VelocityComponent{Velocity = Constants.BigAsteroidVelocity});
        Components.Add(new IntersectionComponent());
        Components.Add(new EnemyComponent{Score = Constants.BigAsteroidScore});
    }
}
