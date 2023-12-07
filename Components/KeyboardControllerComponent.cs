
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

        if(sprite == null)
            return;
        if (transform == null)
            return;


        _previousKey = _currentKey;
        _currentKey = Keyboard.GetState();

        transform.direction = new Vector2((float)Math.Cos(transform.rotation), (float)Math.Sin(transform.rotation));

        

        if (transform != null)
        {
            if (_currentKey.IsKeyDown(Keys.W))
            {
                sprite.velocity += new Vector2(0, -sprite.LinearVelocity);
            }
            if (_currentKey.IsKeyDown(Keys.S))
            {
                sprite.velocity += new Vector2(0, sprite.LinearVelocity);
            }
            if (_currentKey.IsKeyDown(Keys.A))
            {
                sprite.velocity += new Vector2(-sprite.LinearVelocity, 0);
            }
            if (_currentKey.IsKeyDown(Keys.D))
            {
                sprite.velocity += new Vector2(sprite.LinearVelocity, 0);
            }

            if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
            {
                sprite.AddBullet();
                Console.WriteLine("Space");
            }

            // apply velocity
            transform.position += sprite.velocity;

            //get angle from center of screen to mouse in radians
            var mousePosition = Mouse.GetState().Position.ToVector2();
            var angle = (float)Math.Atan2(mousePosition.Y - Game1.ScreenHeight / 2, mousePosition.X - Game1.ScreenWidth / 2);
            angle += (float)Math.PI / 2; //adjust angle to point up

            //set rotation to angle
            transform.rotation = angle;
        }
    }
}