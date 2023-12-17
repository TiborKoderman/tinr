using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ColliderSystem : BaseSystem<ColliderComponent>
{
    //scan for collisions
    public static void Scan()
    {
        foreach (var component in components)
        {
            foreach (var otherComponent in components)
            {
                if (component != otherComponent)
                {
                    if (component.hitbox.Intersects(otherComponent.hitbox))
                    {
                        component.OnCollision(otherComponent);
                    }
                }
            }
        }
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}
