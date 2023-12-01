using System.Collections.Generic;
using Microsoft.Xna.Framework;
using tinr;
public class Entity
{
    public int ID { get; set; }

    public List<Component> components = new();

    public Entity()
    {
        ID = EntityManager.GetNextID();
    }


    public Entity AddComponent(Component component)
    {
        component.entity = this;
        components.Add(component);
        return this;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var component in components)
        {
            if (component is T)
            {
                return (T)component;
            }
        }
        return null;
    }
}