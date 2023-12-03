using Microsoft.Xna.Framework;
using tinr;
class HealthComponent : Component
{
    public int health
    {
        get
        {
            return health;
        }
        set
        {
        }
    }

    public int maxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
        }
    }

    public HealthComponent(int initialHealth)
    {
        maxHealth = initialHealth;
        health = initialHealth;
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
            // entity.Destroy();
        }
    }
}