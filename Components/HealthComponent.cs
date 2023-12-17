using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;
class HealthComponent : Component
{
    public int health { get; set; }


    public int maxHealth
    {
        get; set;
    }
    private TimeSpan lastDamageTime;


    public HealthComponent(int initialHealth)
    {
        maxHealth = initialHealth;
        health = initialHealth;
        lastDamageTime = TimeSpan.Zero;
        HealthSystem.Register(this);
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health + heal > maxHealth)
        {
            health = maxHealth;
            return;
        }
    }


    public override void Update(GameTime gameTime)
    {
        if (health <= 0)
        {
            Destroy();
        }

    }

    public void Destroy()
    {
        EntityManager.RemoveEntity(entity);
        // isRemoved = true;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(entity.GetComponent<EnemyComponent>() == null)
        {
            return;
        }
        var transform = entity.GetComponent<TransformComponent>();
        //draw full healthbar
        spriteBatch.Draw(TextureManager.GetTexture("enemyHealthbar"), new Rectangle((int)transform.position.X-32, (int)transform.position.Y - 40, (int)(64 * ((float)health / (float)maxHealth)), 8), new Rectangle(0, 8, 64, 8), Color.White);
        //draw empty healthbar
        spriteBatch.Draw(TextureManager.GetTexture("enemyHealthbar"), new Rectangle((int)transform.position.X-32, (int)transform.position.Y - 40, 64, 8), new Rectangle(0, 0, 64, 8), Color.White);

    }

}