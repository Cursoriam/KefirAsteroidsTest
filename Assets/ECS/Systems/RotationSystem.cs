public class RotationSystem : ISystem
{
    public RotationSystem()
    {
        SystemEventManager.Instance.Subscribe(Constants.RotateLeftAction, RotateLeft);
        SystemEventManager.Instance.Subscribe(Constants.RotateRightAction, RotateRight);
    }

    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var rotatingComponent = ComponentManager.Instance.GetComponent<RotatingComponent>(entity.EntityId);
            var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);
            if (rotatingComponent != null && playerComponent != null)
            {
                var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
                if (transformComponent != null)
                {
                    if ((Utilities.ContainsInput(playerComponent.Inputs, Constants.RotateLeftAction) ||
                        Utilities.ContainsInput(playerComponent.Inputs, Constants.RotateRightAction))
                        && !Utilities.ContainsInput(playerComponent.Inputs, Constants.AccelerateAction))
                    {
                        continue;
                    }
                    rotatingComponent.RotationAngle = transformComponent.Angle;
                }
            }
        }
    }
    
    private void RotateLeft()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            IncreaseAngle(entity.EntityId, Constants.RotationSpeed);
        }
    }


    private void RotateRight()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            IncreaseAngle(entity.EntityId, -Constants.RotationSpeed);
        }
    }

    private void IncreaseAngle(string entityId, float angle)
    {
        var rotatingComponent = ComponentManager.Instance.GetComponent<RotatingComponent>(entityId);
        var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entityId);
        if (rotatingComponent == null || playerComponent == null) return;
        var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entityId);
        transformComponent.Angle += angle;
        transformComponent.Angle %= (float)(Constants.FloatTwo * Constants.PIInDegrees);
    }
}
