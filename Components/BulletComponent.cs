using Microsoft.Xna.Framework;

class BulletComponent : Component
{
    public float speed = 10f;

    public override void Update(GameTime gameTime)
    {
        var transform = entity.GetComponent<TransformComponent>();

        if (transform != null)
        {
            transform.position += transform.direction * speed;
        }
    }
}