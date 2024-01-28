using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tinr;

public class Player : Entity
{


    public Player()
    {
        AddComponent(new TransformComponent() { position = new Vector2(0, 0) });
        AddComponent(new SpriteComponent(TextureManager.GetTexture("player")));
        AddComponent(new KeyboardControllerComponent());
        AddComponent(new GamepadControllerComponent());
        AddComponent(new CameraComponent());
        AddComponent(new HealthComponent(100));
        AddComponent(new ScoreComponent());
        AddComponent(new ColliderComponent() { bbNormalized = new Rectangle(0, 0, 64, 64) });

        GetComponent<SpriteComponent>().firerate = 2f;
    }


}