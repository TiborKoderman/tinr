using Microsoft.Xna.Framework;

public class Demon : Entity
{
    public Demon()
    {
        AddComponent(new TransformComponent() { position = new Vector2(100, 100) });
        AddComponent(new SpriteComponent(TextureManager.GetTexture("enemy")));
        AddComponent(new EnemyComponent());
        AddComponent(new HealthComponent(100));
        AddComponent(new ColliderComponent() { hitboxNormalised = new Rectangle(0, 0, 64, 64) });
        AddComponent(new DemonAiComponent());
    }

    public Demon(Vector2 position)
    {
        AddComponent(new TransformComponent() { position = position });
        AddComponent(new SpriteComponent(TextureManager.GetTexture("enemy")));
        AddComponent(new EnemyComponent());
        AddComponent(new HealthComponent(100));
        AddComponent(new ColliderComponent() { hitboxNormalised = new Rectangle(0, 0, 64, 64) });
        AddComponent(new DemonAiComponent());
    }
}