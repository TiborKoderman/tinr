using Microsoft.Xna.Framework;

class Bullet : Entity
{
    public Bullet(Vector2 position, float rotation, TransformComponent transform, Rectangle sourceRectangle)
    {
        AddComponent(new TransformComponent()
        {
            // position = transform.position,
            //offset the position to half the top edge of the sprite
            position = transform.position + transform.direction * sourceRectangle.Height / 2,
            rotation = transform.rotation
        })
        .AddComponent(new SpriteComponent(TextureManager.GetTexture("bullet")){
            lifeTime = 3f})
        .AddComponent(new HealthComponent(1))
        .AddComponent(new ColliderComponent(){
            hitboxNormalised = new Rectangle(0,0,sourceRectangle.Width,sourceRectangle.Height)
        });
        var sprite = GetComponent<SpriteComponent>();
        // set bullet velocity to forward at max speed
        //transpose direction to bullet
        sprite.maxVelocity = 15;
        sprite.velocity = transform.direction * sprite.maxVelocity * 3;
        sprite.friction = 0f;
    }
}