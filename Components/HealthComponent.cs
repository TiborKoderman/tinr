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
            health = value;
            if (health <= 0)
            {
                // entity.Destroy();
            }
        }
    }

    public HealthComponent(int initialHealth)
    {
        health = initialHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    public void Heal(int heal)
    {
        health += heal;
    }
}