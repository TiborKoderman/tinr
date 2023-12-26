using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;
class SpriteComponent : Component
{
    public Texture2D _texture;
    public TransformComponent transform;
    public Rectangle sourceRectangle;

    public float RotationVelocity = 3f;
    public float LinearVelocity = 1f;

    public float friction { get; set; }

    public float firerate = 1; //bullets per second

    public float? lifeTime = null;

    

    public Rectangle rectangle
    {
        get
        {
            return new Rectangle((int)transform.position.X, (int)transform.position.Y, _texture.Width, _texture.Height);
        }
        set
        {
        }
    }

    public Vector2 _origin;

    public Vector2 velocity = Vector2.Zero;

    public float maxVelocity = 5;

    public SpriteComponent(Texture2D texture)
    {
        _texture = texture;
        sourceRectangle = new Rectangle(0, 0, 64, 64);
        rectangle = new Rectangle(0, 0, 64, 64);

        friction = 0.3f;

        //set the origin to the center of the sprite
        _origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

        SpriteSystem.Register(this);
    }




    public override void Update(GameTime gameTime)
    {
        transform = entity.GetComponent<TransformComponent>();

        if (velocity.Length() > maxVelocity)
        {
            velocity = Vector2.Normalize(velocity) * maxVelocity;
        }

        velocity *= 1 - friction;

        if (transform != null)
        {
            transform.position += velocity;
        }

        if (lifeTime != null)
        {
            lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifeTime <= 0)
            {
                Destroy();
            }
        }


    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Rotate the sprite 90 degrees counterclockwise

        // Draw the sprite
        if (transform != null)
            spriteBatch.Draw(_texture, transform.position, sourceRectangle, Color.White, transform.rotation, _origin, transform.scale, SpriteEffects.None, 0f);
    }

    public void AddBullet()
    {
        var bullet = new Bullet(transform, sourceRectangle, ref entity);
        children.Add(bullet);

        EntityManager.AddEntity(bullet);
    }

    public void AddBullet(float rotation)
    {
        var bullet = new Bullet(transform, sourceRectangle, ref entity, rotation);
        children.Add(bullet);

        EntityManager.AddEntity(bullet);
    }

    public void Destroy()
    {
        EntityManager.RemoveEntity(entity);
        // isRemoved = true;

    }

}
