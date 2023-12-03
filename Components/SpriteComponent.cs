using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;
class SpriteComponent : Component
{
    public string _textureName;
    public Texture2D _texture;
    public TransformComponent transform;
    public Rectangle sourceRectangle;

    public float RotationVelocity = 3f;
    public float LinearVelocity = 4f;


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

    private Vector2 velocity = Vector2.Zero;

    public SpriteComponent(Texture2D texture)
    {
        _texture = texture;
        sourceRectangle = new Rectangle(0, 0, 64, 64);
        rectangle = new Rectangle(0, 0, 64, 64);

        //set the origin to the center of the sprite
        _origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

        SpriteSystem.Register(this);
    }




    public override void Update(GameTime gameTime)
    {
        transform = entity.GetComponent<TransformComponent>();

        if (transform != null)
        {
            transform.position += velocity;
        }


    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //draw the sprite
        if (transform != null)
            spriteBatch.Draw(_texture, transform.position, sourceRectangle, Color.White, transform.rotation, _origin, transform.scale, SpriteEffects.None, 0f);
    }

    public void AddBullet()
    {
        var bullet = new Entity();
        bullet.AddComponent(new TransformComponent()
        {
            position = transform.position,
            rotation = transform.rotation
        })
        .AddComponent(new SpriteComponent(Game1.game.textures["bullet"]))
        .AddComponent(new HealthComponent(1))
        .AddComponent(new BulletComponent());
        EntityManager.AddEntity(bullet);
    }

}