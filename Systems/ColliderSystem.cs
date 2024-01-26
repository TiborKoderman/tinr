using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ColliderSystem : BaseSystem<ColliderComponent>
{
    //scan for collisions
    public static void Scan()
    {
        Parallel.ForEach(components, component =>
        {
            foreach (var otherComponent in components)
            {
                if (component != otherComponent)
                {
                    if (component.bounds.Intersects(otherComponent.bounds))
                    {
                        component.OnCollision(otherComponent);
                    }
                }
            }
        });
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}


//        