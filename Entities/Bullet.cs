using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

class Bullet : Entity
{
    public Bullet(TransformComponent transform, Rectangle sourceRectangle, ref Entity parent, float? rotation = null)
    {
        this.parent = parent;
        // if(rotation == null)
        // {
        //     rotation = transform.rotation;
        // }
        var direction = transform.direction;
        if(rotation != null)
        {
            direction = Vector2.Transform(direction, Matrix.CreateRotationZ((float)rotation));
        }
        AddComponent(new TransformComponent()
        {
            // position = transform.position,
            //offset the position to half the top edge of the sprite
            
            position = transform.position + direction * sourceRectangle.Height / 2,
            rotation = rotation??transform.rotation,
        });
        AddComponent(new SpriteComponent(TextureManager.GetTexture("bullet")){
            lifeTime = 3f});
        AddComponent(new HealthComponent(1));
        AddComponent(new ColliderComponent(new Rectangle(21,29,43-21,35-29), new Vector2(0,0)){});
        var sprite = GetComponent<SpriteComponent>();
        // set bullet velocity to forward at max speed
        //transpose direction to bullet
        sprite.maxVelocity = 15;
        sprite.velocity = direction * sprite.maxVelocity * 3;
        sprite.friction = 0f;
        TextureManager.GetSound("gunshot").Play();
    }

    ~Bullet()
    {
        parent.GetComponent<SpriteComponent>().children.Remove(this);
    }
}