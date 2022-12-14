using System.Collections.Generic;
using System.Linq;

public class SystemManager
{
    private readonly List<ISystem> _systems;
    private static SystemManager _systemManager;
    
    public static SystemManager Instance
    {
        get { return _systemManager ??= new SystemManager(); }
    }

    private SystemManager()
    {
        _systems = new List<ISystem>();
    }

    public void AddSystem(ISystem system)
    {
        _systems.Add(system);
    }

    public T GetSystem<T>()
    {
        if(_systems.Where(item => item.GetType() == typeof(T)).ToList().Count > 0)
            return (T)_systems.Where(item => item.GetType() == typeof(T)).ToList()[0];
        return default;
    }
    
    public void RemoveSystem<T>()
    {
        _systems.Remove((ISystem)typeof(T));
    }
    
    // Update is called once per frame
    public void Update()
    {
        foreach (var system in _systems)
        {
            system.Update();
        }
    }
}
