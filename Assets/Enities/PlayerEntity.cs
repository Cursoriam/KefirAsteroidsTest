using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public PlayerEntity(): base(){}
    public PlayerEntity(string entityId) : base(entityId)
    {
        Components.Add(new TransformComponent{Size = Constants.PlayerSize});
        Components.Add(new VelocityComponent{Acceleration = Constants.PlayerAcceleration});
        Components.Add(new RotatingComponent());
        Components.Add(new PlayerComponent());
        Components.Add(new DestructibleComponent());
        Components.Add(new PlayerComponent());
    }
}
