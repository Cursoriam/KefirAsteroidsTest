using Random = System.Random;


public class LittleAsteroidEntity : AsteroidEntity
{
    public LittleAsteroidEntity(): base(){}

    public LittleAsteroidEntity(string entityId) : base(entityId)
    {
        Random = new Random();
        Components.Add(new TransformComponent{Angle = Random.Next(Constants.IntZero,
                (int)Constants.PIInDegrees), Size = Constants.LittleAsteroidSize});
        Components.Add(new VelocityComponent{Velocity = Constants.LittleAsteroidVelocity});
        Components.Add(new EnemyComponent{Score = Constants.LittleAsteroidScore});
    }
}
