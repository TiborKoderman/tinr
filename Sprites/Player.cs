using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace tinr.Sprites{
    public class Player : Sprite
    {
        enum State
        {
            Walking,
            Attacking,
            Idle,
            Dashing
        }

        // Direction direction = Direction.D;
        // State state = State.Idle;

        public Bullet Bullet;

        public Player(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();


            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));  

            if(_currentKey.IsKeyDown(Keys.A))
            {
                Position += LinearVelocity * new Vector2(-1,0);
            }
            if (_currentKey.IsKeyDown(Keys.D))
            {
                Position -= LinearVelocity * new Vector2(-1, 0);
            }
            if (_currentKey.IsKeyDown(Keys.W))
            {
                Position += LinearVelocity * new Vector2(0, -1);
            }
            if (_currentKey.IsKeyDown(Keys.S))
            {
                Position -= LinearVelocity * new Vector2(0, -1);
            }



            if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
            {
                AddBullet(sprites);
            }

            //update position
            // Position += Direction * LinearVelocity;
            }

        private void AddBullet(List<Sprite> sprites)
        {
                Bullet = Bullet.Clone() as Bullet;
                //rotate 90 degrees counter clockwise
                Bullet._rotation = _rotation - (float)(Math.PI / 2);
                Bullet.Direction = new Vector2((float)Math.Cos(Bullet._rotation), (float)Math.Sin(Bullet._rotation));
                Bullet.Position = this.Position;
                Bullet.LinearVelocity = this.LinearVelocity*2;
                Bullet.LifeSpan = 2f;
                Bullet.Parent = this;

                sprites.Add(Bullet);
        }

    }
}