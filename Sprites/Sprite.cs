using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tinr.Sprites{
    public class Sprite : Component, ICloneable 
    {
        protected Texture2D _texture;
        // public float Rotation {get; set;} = 0f;
        public float _rotation = 0f;       
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Vector2 Position;
        public Vector2 Origin;

        public Vector2 Scale {get; set;} = new Vector2(1,1);
        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;

        public Sprite Parent;

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        //origin is the center of the sprite


        public Rectangle Rectangle{
            get{ return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //draw the sprite
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, Scale, SpriteEffects.None, 0f);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width/2, _texture.Height/2);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}