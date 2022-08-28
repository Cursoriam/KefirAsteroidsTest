using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public Action<Coordinates2D, float> GetPlayerTransformAction;
    public Action<float> GetPlayerVelocityAction;
    
    public PlayerModel()
    {
        SystemManager.Instance.GetSystem<TransformSystem>().PlayerTransformChanged += GetPlayerTransform;
        SystemManager.Instance.GetSystem<VelocitySystem>().PlayerVelocityChangedAction += GetPlayerVelocity;
    }
    
    public void SendInput(string input)
    {
        SystemManager.Instance.GetSystem<InputSystem>().AddInput(input);
    }

    private void GetPlayerTransform(Coordinates2D position, float angle)
    {
        GetPlayerTransformAction?.Invoke(position, angle);
    }

    private void GetPlayerVelocity(float velocity)
    {
        GetPlayerVelocityAction?.Invoke(velocity);
    }
}
