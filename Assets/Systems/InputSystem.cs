using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : ISystem
{
    public InputSystem()
    {
        EntityManager.GetInstance().CreateEntity<InputEntity>("inputEntity");
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.GetInstance().GetAllEntities())
        {
            var inputComponent = ComponentManager.GetInstance().GetComponent<InputComponent>(entity.EntityId);
            if (inputComponent is {Input: { }})
            {
                SystemEventManager.GetInstance().Trigger(
                    ComponentManager.GetInstance().GetComponent<InputComponent>(entity.EntityId).Input);
                ComponentManager.GetInstance().GetComponent<InputComponent>(entity.EntityId).Input = null;
            }

        }
    }

    public void AddInput(string input)
    {
        foreach (var entity in EntityManager.GetInstance().GetAllEntities())
        {
            if(ComponentManager.GetInstance().GetComponent<InputComponent>(entity.EntityId) != null)
                ComponentManager.GetInstance().GetComponent<InputComponent>(entity.EntityId).Input = input;   
        }
    }
}
