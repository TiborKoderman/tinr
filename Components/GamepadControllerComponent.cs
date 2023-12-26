
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tinr;
class GamepadControllerComponent : Component
{

    private TimeSpan lastBulletTime = TimeSpan.Zero;

    public GamepadControllerComponent()
    {
        lastBulletTime = TimeSpan.Zero;

    }

public override void Update(GameTime gameTime)
{
    TransformComponent transform = entity.GetComponent<TransformComponent>();
    SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

    if(sprite == null || transform == null)
        return;

    float firerate = sprite.firerate;

    GamePadState currentState = GamePad.GetState(PlayerIndex.One);
    if (!currentState.IsConnected)
        return;

    // Use the left thumbstick to move
    Vector2 leftThumbstick = currentState.ThumbSticks.Left;
    sprite.velocity += new Vector2(leftThumbstick.X, -leftThumbstick.Y) * sprite.LinearVelocity;

    // Use the right thumbstick to rotate
    Vector2 rightThumbstick = currentState.ThumbSticks.Right;
    if (rightThumbstick != Vector2.Zero)
    {
        float angle = (float)Math.Atan2(rightThumbstick.Y, rightThumbstick.X);
        angle -= MathHelper.PiOver2; // Adjust angle to point up
        transform.rotation = -angle;
    }

    // Fire at firerate if
    if (currentState.Triggers.Right > 0.5f)
    {
        if(gameTime.TotalGameTime - lastBulletTime > TimeSpan.FromSeconds(1 / firerate))
        {
            sprite.AddBullet();
            lastBulletTime = gameTime.TotalGameTime;
        }
    }

    // Apply velocity
    transform.position += sprite.velocity;
}
}