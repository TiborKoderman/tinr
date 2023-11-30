using System.Collections.Generic;
using Microsoft.Xna.Framework;
using tinr;
public class Entity
{
    public int ID { get; set; }

    List<Component> components = new List<Component>();

    public void AddComponent(Component component)
    {
        component.entity = this;
        components.Add(component);
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