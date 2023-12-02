using Microsoft.Xna.Framework;
using tinr;
class ColliderComponent : Component
{
    public bool isTrigger = false;
    public Rectangle Collider = new Rectangle(0, 0, 0, 0);

    TransformComponent transform;

    public ColliderComponent()
    {
        ColliderSystem.Register(this);
    }

    public void Update(GameTime gameTime)
    {
        transform = entity.GetComponent<TransformComponent>();
        Collider.X = (int)transform.position.X;
        Collider.Y = (int)transform.position.Y;
    }
}