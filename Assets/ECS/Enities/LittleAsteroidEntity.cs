using Random = System.Random;


public class LittleAsteroidEntity : AsteroidEntity
{
    public LittleAsteroidEntity() {}

    public LittleAsteroidEntity(string entityId) : base(entityId)
    {
        Components.Add(new TransformComponent{Angle = Random.Next(Constants.IntZero,
                (int)Constants.PIInDegrees), Size = Constants.LittleAsteroidSize});
        Components.Add(new VelocityComponent{Velocity = Constants.LittleAsteroidVelocity});
        Components.Add(new EnemyComponent{Score = Constants.LittleAsteroidScore});
    }
}
