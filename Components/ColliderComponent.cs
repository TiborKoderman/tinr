using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;
class ColliderComponent : Component
{
    public bool isTrigger = false;
    public Rectangle hitboxNormalised = new Rectangle(0, 0, 64, 64);
    TransformComponent transform;

    public Rectangle hitbox
    {
        get
        {
            return new Rectangle((int)transform.position.X, (int)transform.position.Y, hitboxNormalised.Width, hitboxNormalised.Height);
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
        transform = entity.GetComponent<TransformComponent>();

    }

    public void OnCollision(ColliderComponent other)
    {
        Console.WriteLine("Collision");
        entity.GetComponent<HealthComponent>().Damage(10);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });

        spriteBatch.Draw(texture, hitbox, Color.Red);
    }
}