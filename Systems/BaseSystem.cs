using System.Collections.Generic;
using Microsoft.Xna.Framework;
using tinr;

class BaseSystem<T> where T : Component
{
    public static List<T> components = new List<T>();

    public static void getComponents(ref List<T> components)
    {
        components = BaseSystem<T>.components;
    }

    public static void Register(T component)
    {
        components.Add(component);
    }

    public static void Unregister(T component)
    {
        components.Remove(component);
    }

    public static void Update(GameTime gameTime)
    {
        foreach (T component in components.ToArray())
        {
            component.Update(gameTime);
        }
    }

    public static void Cleanup()
    {
        components.Clear();
    }
}
