
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tinr;
class KeyboardControllerComponent : Component
{

    private KeyboardState _previousKey;
    private KeyboardState _currentKey;

    public override void Update(GameTime gameTime)
    {
        var transform = entity.GetComponent<TransformComponent>();

        var sprite = entity.GetComponent<SpriteComponent>();


        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();

        if (transform != null)
        {
            if (_currentKey.IsKeyDown(Keys.W))
            {
                transform.position.Y -= 1;
            }
            if (_currentKey.IsKeyDown(Keys.S))
            {
                transform.position.Y += 1;
            }
            if (_currentKey.IsKeyDown(Keys.A))
            {
                transform.position.X -= 1;
            }
            if (_currentKey.IsKeyDown(Keys.D))
            {
                transform.position.X += 1;
            }

            if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
            {
                // AddBullet(sprites);
                Console.WriteLine("Space");
            }
        }
    }
}