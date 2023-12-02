using Microsoft.Xna.Framework;

class ColliderSystem : BaseSystem<ColliderComponent>
{
    public void Update(GameTime gameTime)
    {
        foreach (var component in components)
        {
            component.Update(gameTime);
        }
    }

    private bool CheckCollision(ColliderComponent a, ColliderComponent b)
    {
        return a.Collider.Intersects(b.Collider);
    }


}