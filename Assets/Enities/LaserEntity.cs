using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEntity : Entity
{
    public LaserEntity(): base(){}

    public LaserEntity(string entityId): base(entityId)
    {
        Components.Add(new TransformComponent{Size = Constants.LaserSize});
        Components.Add(new LaserComponent());
        Components.Add(new DestructibleComponent());
    }
}
