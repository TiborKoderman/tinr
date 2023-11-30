using System.Collections.Generic;
using Microsoft.Xna.Framework;
using tinr;
public class Entity : Game1
{
    public int ID { get; set; }

    public List<Component> components = new();

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