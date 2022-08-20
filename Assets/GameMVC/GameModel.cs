using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public GameModel()
    {
        SystemManager.GetInstance().AddSystem(new VelocitySystem());
        SystemManager.GetInstance().AddSystem(new TransformSystem());
    }
    // Update is called once per frame
    public void Update()
    {
        SystemManager.GetInstance().Update();
    }
}
