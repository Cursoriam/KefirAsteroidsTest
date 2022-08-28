using System;

public class PlayerModel
{
    public Action<Coordinates2D, float> GetPlayerTransformAction;
    public Action<float> GetPlayerVelocityAction;
    
    public PlayerModel()
    {
        SystemManager.Instance.GetSystem<TransformSystem>().PlayerTransformChangedAction += PlayerTransformChanged;
        SystemManager.Instance.GetSystem<VelocitySystem>().PlayerVelocityChangedAction += PlayerVelocityChanged;
    }
    
    public void SendInput(string input)
    {
        SystemManager.Instance.GetSystem<InputSystem>().AddInput(input);
    }

    private void PlayerTransformChanged(Coordinates2D position, float angle)
    {
        GetPlayerTransformAction?.Invoke(position, angle);
    }

    private void PlayerVelocityChanged(float velocity)
    {
        GetPlayerVelocityAction?.Invoke(velocity);
    }
}
