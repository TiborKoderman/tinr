
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

        transform.direction = new Vector2((float)Math.Cos(transform.rotation), (float)Math.Sin(transform.rotation));  

        if (transform != null)
        {
            if (_currentKey.IsKeyDown(Keys.W))
            {
                transform.position += sprite.LinearVelocity * new Vector2(0,-1);
            }
            if (_currentKey.IsKeyDown(Keys.S))
            {
                transform.position -= sprite.LinearVelocity * new Vector2(0,-1);
            }
            if (_currentKey.IsKeyDown(Keys.A))
            {
                transform.position += sprite.LinearVelocity * new Vector2(-1,0);
            }
            if (_currentKey.IsKeyDown(Keys.D))
            {
                transform.position -= sprite.LinearVelocity * new Vector2(-1,0);
            }

            if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
            {
                sprite.AddBullet();
                Console.WriteLine("Space");
            }

            

            // look towards the mouse
            // var mouseState = Mouse.GetState();
            // var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            // var direction = mousePosition - transform.position;
            // direction.Normalize();
            // transform.rotation = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
}