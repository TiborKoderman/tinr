using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;

class ColliderComponent : Component
{
    public bool isTrigger = false;
    public Rectangle hitboxNormalised = new Rectangle(0, 0, 64, 64);
    private TransformComponent transform;

    public BoundingBox bounds
    {
        get
        {
            transform = entity.GetComponent<TransformComponent>();
            int centerX = (int)transform.position.X - hitboxNormalised.Width / 2;
            int centerY = (int)transform.position.Y - hitboxNormalised.Height / 2;
            return new BoundingBox(new Vector3(centerX, centerY, 0), new Vector3(centerX + hitboxNormalised.Width, centerY + hitboxNormalised.Height, 0));
        }
        set
        {
        }
    }



    public ColliderComponent()
    {
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

        //draw the bounding box
        BoundingBox box = bounds;
        Vector3[] corners = box.GetCorners();
        for (int i = 0; i < 4; i++)
        {
            Vector3 corner1 = corners[i];
            Vector3 corner2 = corners[(i + 1) % 4];
            spriteBatch.Draw(hitboxTexture, new Rectangle((int)corner1.X, (int)corner1.Y, (int)(corner2 - corner1).Length(), 1), null, Color.White, (float)Math.Atan2(corner2.Y - corner1.Y, corner2.X - corner1.X), Vector2.Zero, SpriteEffects.None, 0);
        }


    }
}