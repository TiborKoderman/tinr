using System;
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

        enum Direction
        {
            U,
            D,
            L,
            R,
            UL,
            UR,
            DL,
            DR
        }

        // Direction direction = Direction.D;
        // State state = State.Idle;

        public Player(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var velocity = new Vector2();

            var speed = 3f;


            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y += speed;
            }

            var touchState = TouchPanel.GetState();
            if (touchState.Count > 0)
            {
                var touchPosition = touchState[0].Position;
                var touchDirection = touchPosition - Position;
                touchDirection.Normalize();
                velocity += touchDirection * speed;
            }

            Position += velocity;
            
                        
        }

    }
}