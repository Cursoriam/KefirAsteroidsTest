using System;
using System.Linq;

public class DestructionSystem : ISystem
{
    public Action<string> EntityDestroyedAction;
    public Action PlayerDestroyedAction;
    public Action<int> ScoreSentAction;
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            var destructibleComponent = ComponentManager.Instance.GetComponent<DestructibleComponent>(entity.EntityId);
            var playerComponent = ComponentManager.Instance.GetComponent<PlayerComponent>(entity.EntityId);
            if (destructibleComponent is {NeedToDestroy: true})
            {
                var enemyComponent = ComponentManager.Instance.GetComponent<EnemyComponent>(entity.EntityId);
                
                if(enemyComponent != null)
                    ScoreSentAction?.Invoke(enemyComponent.Score);
                
                EntityDestroyedAction?.Invoke(entity.EntityId);
                if (playerComponent != null)
                {
                    PlayerDestroyedAction?.Invoke();
                }
                EntityManager.Instance.RemoveEntity(entity.EntityId);
            }
            
            if (playerComponent != null && !Utilities.IsEmpty(playerComponent.Inputs))
            {
                foreach (var input in playerComponent.Inputs.ToList())
                {
                    playerComponent.Inputs.Remove(input);
                }
            }
        }
    }

    public void DestroyAllEntities()
    {
        foreach (var entity in EntityManager.Instance.GetAll())
        {
            EntityDestroyedAction?.Invoke(entity.EntityId);
            EntityManager.Instance.RemoveEntity(entity.EntityId);
        }
    }
}
