using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : ISystem
{
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var inputComponent = ComponentManager.Instance.GetComponent<InputComponent>(entity.EntityId);
            if (inputComponent != null && Utilities.IsEmpty(inputComponent.Inputs))
            {
                foreach (var input in inputComponent.Inputs)
                {
                    SystemEventManager.Instance.Trigger(input);
                    inputComponent.Inputs.Remove(input);
                }
            }
        }
    }

    public void AddInput(string input)
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var inputComponent = ComponentManager.Instance.GetComponent<InputComponent>(entity.EntityId);
            inputComponent?.Inputs.Add(input);
        }
    }
}
