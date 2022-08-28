using Random = System.Random;

public class AsteroidEntity : Entity
{
    protected static Random Random = new Random();
    protected AsteroidEntity() {}

    protected AsteroidEntity(string entityId) : base(entityId)
    {
        Components.Add(new DestructibleComponent());
    }
}
