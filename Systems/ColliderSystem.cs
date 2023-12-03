using Microsoft.Xna.Framework;

class ColliderSystem : BaseSystem<ColliderComponent>
{
    public static void Update(GameTime gameTime)
    {
        foreach (var component in components)
        {
            component.Update(gameTime);
        }
    }

    // public static void Unregister(ColliderComponent component)
    // {
    //     components.Remove(component);
    // }

    private bool CheckCollision(ColliderComponent a, ColliderComponent b)
    {
        return a.Collider.Intersects(b.Collider);
    }


}