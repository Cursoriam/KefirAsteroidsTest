using System.Linq;

public class InputSystem : ISystem
{
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var inputComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);
            if (inputComponent != null && !Utilities.IsEmpty(inputComponent.Inputs))
            {
                foreach (var input in inputComponent.Inputs.ToList())
                {
                    SystemEventManager.Instance.Trigger(input);
                }
            }
        }
    }

    public void AddInput(string input)
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var inputComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);
            
            inputComponent?.Inputs.Add(input);
        }
    }
}
