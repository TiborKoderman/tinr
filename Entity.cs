using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using tinr;
public class Entity
{
    public UInt64 ID { get; set; }

    public List<Component> components = new();

    public List<Entity> children = new();

    public Entity parent;

    public float lifeSpan = 0f;

    public bool isRemoved = false;

    public Entity()
    {

    }


    public Entity AddComponent(Component component)
    {
        component.entity = this;
        components.Add(component);
        return this;
    }

    // public Entity AddComponent(string componentName)
    // {
    //     var component = (Component)Activator.CreateInstance(Type.GetType("tinr." + componentName));
    //     component.entity = this;
    //     components.Add(component);
    //     return this;
    // }

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

    public void Cleanup()
    {
        foreach (var component in components)
        {
            typeof(BaseSystem<>).MakeGenericType(component.GetType()).GetMethod("Unregister").Invoke(null, new object[] { component });
        }
        components.Clear();
    }

    ~Entity()
    {
        Cleanup();
    }
}