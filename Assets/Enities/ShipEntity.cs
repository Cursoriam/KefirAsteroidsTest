using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEntity : Entity
{
    public ShipEntity(): base(){}
    public ShipEntity(string entityId) : base(entityId)
    {
        _components.Add(new Transform());
        var velocityComponent = new VelocityComponent();
        velocityComponent.Acceleration = -0.1f;
        _components.Add(velocityComponent);
    }
}
