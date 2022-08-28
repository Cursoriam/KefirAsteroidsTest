public class BulletEntity : Entity
{
    public BulletEntity(): base(){}

    public BulletEntity(string entityId) : base(entityId)
    {
        Components.Add(new TransformComponent{Size = Constants.BulletSize});
        Components.Add(new VelocityComponent {Velocity = Constants.BulletVelocity});
        Components.Add(new BulletComponent());
        Components.Add(new DestructibleComponent());
    }
}
