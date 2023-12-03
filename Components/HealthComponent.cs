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

        if(gameTime.TotalGameTime - lastDamageTime > TimeSpan.FromSeconds(1))
        {
            lastDamageTime = gameTime.TotalGameTime;
            Damage(20);
        }

    }

    public void Destroy()
    {
        EntityManager.RemoveEntity(entity);
        // isRemoved = true;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }

}