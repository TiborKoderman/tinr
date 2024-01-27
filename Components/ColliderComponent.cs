using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;

class ColliderComponent : Component
{
    public bool isTrigger = false;
    public Rectangle bbNormalized = new Rectangle(0, 0, 64, 64);
    private TransformComponent transform;

    public Vector2 _origin = new(0, 0);


    public Rectangle bounds
    {
        get
        {
            transform = entity.GetComponent<TransformComponent>();
            Vector2 position = transform.position;
            return new Rectangle((int)position.X + bbNormalized.X, (int)position.Y + bbNormalized.Y, bbNormalized.Width, bbNormalized.Height);
        }
        set
        {
        }
    }



    public ColliderComponent()
    {
        _origin = new Vector2(bbNormalized.Width / 2, bbNormalized.Height / 2);
        ColliderSystem.Register(this);
    }

    public ColliderComponent(Rectangle bounds)
    {
        bbNormalized = bounds;
        _origin = new Vector2(bbNormalized.Width / 2, bbNormalized.Height / 2);
        ColliderSystem.Register(this);
    }

    public ColliderComponent(Rectangle bounds, Vector2 origin)
    {
        bbNormalized = bounds;
        _origin = origin;
        ColliderSystem.Register(this);
    }


    public override void Update(GameTime gameTime)
    {
        // transform = entity.GetComponent<TransformComponent>();
    }

    public void OnCollision(ColliderComponent other)
    {
        if (other == this.parent || other.children.Contains(this) || other.entity.children.Contains(this))
        {
            return;
        }
        Console.WriteLine("Collision");
        entity.GetComponent<HealthComponent>().Damage(10);
    }

    // Draw the hitbox at the center of the entity
    public void Draw(SpriteBatch spriteBatch)
    {
        Texture2D hitboxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        hitboxTexture.SetData(new[] { Color.Red * 0.5f });

        // Rotate the bounding box by 90 degrees

        // Draw the bounding box
        if (transform != null){
            float rotation = transform.rotation + MathHelper.ToRadians(90);
            spriteBatch.Draw(hitboxTexture, transform.position, bbNormalized, Color.White, rotation, _origin, transform.scale, SpriteEffects.None, 0f);

        }
    }
}