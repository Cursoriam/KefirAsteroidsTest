using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            var rotationableComponent = ComponentManager.Instance.GetComponent<RotationableComponent>(entity.EntityId);
            if (rotationableComponent == null) continue;
            var inputComponent = ComponentManager.Instance.GetComponent<InputComponent>(entity.EntityId);
            if (inputComponent == null ||
                Utilities.ContainsInput(inputComponent.Inputs, Constants.AccelerateAction)) continue;
            var transformComponent =
                ComponentManager.Instance.GetComponent<TransformComponent>(entity.EntityId);
            if (transformComponent != null)
            {
                rotationableComponent.rotationAngle = transformComponent.Angle;
            }
        }
    }
    
    private void RotateLeft()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            IncreaseAngle(entity.EntityId, -Constants.RotationSpeed);
        }
    }


    private void RotateRight()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            IncreaseAngle(entity.EntityId, Constants.RotationSpeed);
        }
    }

    private void IncreaseAngle(string entityId, float angle)
    {
        var rotationableComponent = ComponentManager.Instance.GetComponent<RotationableComponent>(entityId);
        if (rotationableComponent == null) return;
        var transformComponent = ComponentManager.Instance.GetComponent<TransformComponent>(entityId);
        if (transformComponent != null)
        {
            transformComponent.Angle += angle;
        }
    }
}
